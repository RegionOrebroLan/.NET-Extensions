using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RegionOrebroLan.Abstractions.Extensions;
using RegionOrebroLan.Collections.Generic.Extensions;
using RegionOrebroLan.Extensions;
using RegionOrebroLan.Security.Cryptography.Validation.Configuration;
using RegionOrebroLan.Validation;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public class CertificateValidator : ICertificateValidator
	{
		#region Methods

		protected internal virtual X509Chain CreateChain(bool? useMachineContext)
		{
			// ReSharper disable ConvertIfStatementToReturnStatement
			if(useMachineContext != null)
				return new X509Chain(useMachineContext.Value);
			// ReSharper restore ConvertIfStatementToReturnStatement

			return new X509Chain();
		}

		protected internal virtual X509Chain CreateChain(ICertificate certificate, CertificateValidationOptions options)
		{
			var chainPolicy = this.CreateChainPolicy(certificate, options);

			var chain = this.CreateChain(options?.UseMachineContext);

			chain.ChainPolicy = chainPolicy;

			return chain;
		}

		protected internal virtual X509ChainPolicy CreateChainPolicy(ICertificate certificate, CertificateValidationOptions options)
		{
			if(certificate == null)
				throw new ArgumentNullException(nameof(certificate));

			if(options == null)
				throw new ArgumentNullException(nameof(options));

			var chainPolicy = new X509ChainPolicy();

			foreach(var item in options.ApplicationPolicyObjectIdentifiers)
			{
				chainPolicy.ApplicationPolicy.Add(new Oid(item));
			}

			foreach(var item in options.CertificatePolicyObjectIdentifiers)
			{
				chainPolicy.CertificatePolicy.Add(new Oid(item));
			}

			if(options.RevocationFlag != null)
				chainPolicy.RevocationFlag = options.RevocationFlag.Value;

			if(options.RevocationMode != null)
				chainPolicy.RevocationMode = options.RevocationMode.Value;

			if(options.UrlRetrievalTimeout != null)
				chainPolicy.UrlRetrievalTimeout = options.UrlRetrievalTimeout.Value;

			foreach(var trustedIntermediateCertificate in options.TrustedIntermediateCertificates)
			{
				chainPolicy.ExtraStore.Add(this.UnwrapCertificate(trustedIntermediateCertificate));
			}

			foreach(var trustedRootCertificate in options.TrustedRootCertificates)
			{
				chainPolicy.ExtraStore.Add(this.UnwrapCertificate(trustedRootCertificate));
			}

			if(options.VerificationFlags != null)
				chainPolicy.VerificationFlags = options.VerificationFlags.Value;

			if(options.VerificationTime != null)
				chainPolicy.VerificationTime = options.VerificationTime.Value;

			return chainPolicy;
		}

		protected internal virtual IValidationResult EvaluateMatching(ICertificate certificate, CertificateValidationOptions options)
		{
			var validationResult = new ValidationResult();

			var matching = options?.Matching ?? new MatchingOptions();

			// ReSharper disable InvertIf
			if(matching.Criteria.Any())
			{
				var messagePrefix = $"Certificate {this.ValueAsFormatItem(certificate?.Subject)}: ";
				var results = new List<KeyValuePair<string, bool>>();

				foreach(var criterion in matching.Criteria)
				{
					var formattedPropertyName = this.ValueAsFormatItem(criterion.PropertyName);
					var formattedValuePattern = this.ValueAsFormatItem(criterion.ValuePattern);
					var canNotEvaluateMessagePrefix = $"{messagePrefix}Can not evaluate if certificate-property {formattedPropertyName} matches value {formattedValuePattern} because";

					if(certificate == null)
					{
						results.Add(new KeyValuePair<string, bool>($"{canNotEvaluateMessagePrefix} because the certificate is NULL.", false));
						continue;
					}

					if(!this.TryGetCertificatePropertyValue(certificate, criterion.PropertyName, out var propertyValue))
					{
						results.Add(new KeyValuePair<string, bool>($"{canNotEvaluateMessagePrefix} because the certificate-property does not exist.", false));
						continue;
					}

					var evaluateMessagePrefix = $"{messagePrefix}The value {this.ValueAsFormatItem(propertyValue)}, for certificate-property {formattedPropertyName}";

					if((propertyValue == null && criterion.ValuePattern == null) || (propertyValue != null && propertyValue.Like(criterion.ValuePattern)))
					{
						if(!matching.AllCriteriaShouldMatch)
							return validationResult;

						results.Add(new KeyValuePair<string, bool>($"{evaluateMessagePrefix}, matches value {formattedValuePattern}.", true));
						continue;
					}

					results.Add(new KeyValuePair<string, bool>($"{evaluateMessagePrefix}, do not match value {formattedValuePattern}.", false));
				}

				if((matching.AllCriteriaShouldMatch && results.Any(item => !item.Value)) || (!matching.AllCriteriaShouldMatch && !results.Any(item => item.Value)))
					validationResult.Exceptions.Add(results.Where(item => !item.Value).Select(item => new EvaluateException(item.Key)));
			}
			// ReSharper restore InvertIf

			return validationResult;
		}

		protected internal virtual bool IsSelfSignedCertificate(ICertificate certificate)
		{
			if(certificate == null)
				throw new ArgumentNullException(nameof(certificate));

			Span<byte> subject = certificate.SubjectName.RawData.ToArray();
			Span<byte> issuer = certificate.IssuerName.RawData.ToArray();

			return subject.SequenceEqual(issuer);
		}

		protected internal virtual bool TryGetCertificatePropertyValue(ICertificate certificate, string propertyName, out string propertyValue)
		{
			propertyValue = null;

			// ReSharper disable InvertIf
			if(certificate != null && !string.IsNullOrWhiteSpace(propertyName))
			{
				const StringComparison comparison = StringComparison.OrdinalIgnoreCase;

				Func<string> propertyFunction = null;

				if(propertyName.Equals(nameof(certificate.FriendlyName), comparison))
					propertyFunction = () => certificate.FriendlyName;

				if(propertyName.Equals(nameof(certificate.Issuer), comparison))
					propertyFunction = () => certificate.Issuer;

				if(propertyName.Equals(nameof(certificate.SerialNumber), comparison))
					propertyFunction = () => certificate.SerialNumber;

				if(propertyName.Equals(nameof(certificate.Subject), comparison))
					propertyFunction = () => certificate.Subject;

				if(propertyName.Equals(nameof(certificate.Thumbprint), comparison))
					propertyFunction = () => certificate.Thumbprint;

				if(propertyFunction != null)
				{
					propertyValue = propertyFunction();
					return true;
				}
			}
			// ReSharper restore InvertIf

			return false;
		}

		protected internal virtual X509Certificate2 UnwrapCertificate(ICertificate certificate)
		{
			try
			{
				return certificate.Unwrap<X509Certificate2>();
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException($"Could not unwrap certificate {this.ValueAsFormatItem(certificate?.Subject)}.", exception);
			}
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual async Task<IValidationResult> ValidateAsync(ICertificate certificate, CertificateValidatorOptions options)
		{
			if(certificate == null)
				throw new ArgumentNullException(nameof(certificate));

			if(options == null)
				throw new ArgumentNullException(nameof(options));

			var validationResult = new ValidationResult();

			var isSelfSignedCertificate = this.IsSelfSignedCertificate(certificate);

			if(isSelfSignedCertificate && !options.AllowedCertificateKinds.HasFlag(CertificateKinds.SelfSigned))
				validationResult.Exceptions.Add(new InvalidOperationException("Options do not allow self signed certificates."));

			// ReSharper disable InvertIf
			if(validationResult.Valid)
			{
				if(!isSelfSignedCertificate && !options.AllowedCertificateKinds.HasFlag(CertificateKinds.Chained))
					validationResult.Exceptions.Add(new InvalidOperationException("Options do not allow chained certificates."));

				if(validationResult.Valid)
				{
					var validationOptions = isSelfSignedCertificate ? options.SelfSigned : options.Chained;
					var chain = this.CreateChain(certificate, validationOptions);

					//// Can not find any reason to add it to the extra store.
					//if (isSelfSignedCertificate)
					//	chain.ChainPolicy.ExtraStore.Add(this.UnwrapCertificate(certificate));

					if(!chain.Build(this.UnwrapCertificate(certificate)))
					{
						foreach(var chainStatus in chain.ChainStatus)
						{
							validationResult.Exceptions.Add(new InvalidOperationException($"{chainStatus.Status}: {chainStatus.StatusInformation}"));
						}

						foreach(var chainLink in chain.ChainElements)
						{
							foreach(var chainStatus in chainLink.ChainElementStatus)
							{
								validationResult.Exceptions.Add(new InvalidOperationException($"{chainLink.Certificate.Subject}: {chainStatus.Status} - {chainStatus.StatusInformation}"));
							}
						}
					}

					if(validationOptions.ChainTrustMode == X509ChainTrustMode.CustomRootTrust)
					{
						var certificates = chain.ChainElements.Cast<X509ChainElement>().Select(element => (X509Certificate2Wrapper) element.Certificate).ToArray();

						var intermediateCertificates = certificates.Take(certificates.Length - 1).Skip(1).ToArray();

						foreach(var intermediateCertificate in intermediateCertificates)
						{
							if(!validationOptions.TrustedIntermediateCertificates.Contains(intermediateCertificate))
								validationResult.Exceptions.Add(new InvalidOperationException($"{certificate.Subject}: {nameof(validationOptions.ChainTrustMode)} is set to {validationOptions.ChainTrustMode} and the intermediate-certificate \"{intermediateCertificate.Subject}\" is not in the list of trusted intermediate-certificates."));
						}

						var rootCertificate = certificates.Last();

						if(!validationOptions.TrustedRootCertificates.Contains(rootCertificate))
							validationResult.Exceptions.Add(new InvalidOperationException($"{certificate.Subject}: {nameof(validationOptions.ChainTrustMode)} is set to {validationOptions.ChainTrustMode} and the root-certificate \"{rootCertificate.Subject}\" is not in the list of trusted root-certificates."));
					}

					if(validationResult.Valid)
						validationResult.Exceptions.Add(this.EvaluateMatching(certificate, validationOptions).Exceptions);
				}
			}
			// ReSharper restore InvertIf

			return await Task.FromResult(validationResult).ConfigureAwait(false);
		}

		public virtual async Task<IValidationResult> ValidateAsync(X509Certificate2 certificate, CertificateValidatorOptions options)
		{
			return await this.ValidateAsync((X509Certificate2Wrapper) certificate, options).ConfigureAwait(false);
		}

		protected internal virtual string ValueAsFormatItem(string value)
		{
			return value == null ? "NULL" : $"\"{value}\"";
		}

		#endregion
	}
}