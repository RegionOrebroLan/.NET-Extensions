using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RegionOrebroLan.Abstractions.Extensions;
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

		protected internal virtual X509Chain CreateChain(X509Certificate2 certificate, CertificateValidationOptions options)
		{
			var chainPolicy = this.CreateChainPolicy(certificate, options);

			var chain = this.CreateChain(options?.UseMachineContext);

			chain.ChainPolicy = chainPolicy;

			return chain;
		}

		protected internal virtual X509ChainPolicy CreateChainPolicy(X509Certificate2 certificate, CertificateValidationOptions options)
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

		protected internal virtual bool IsSelfSignedCertificate(X509Certificate2 certificate)
		{
			if(certificate == null)
				throw new ArgumentNullException(nameof(certificate));

			Span<byte> subject = certificate.SubjectName.RawData;
			Span<byte> issuer = certificate.IssuerName.RawData;

			return subject.SequenceEqual(issuer);
		}

		protected internal virtual X509Certificate2 UnwrapCertificate(ICertificate certificate)
		{
			try
			{
				return certificate.Unwrap<X509Certificate2>();
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException($"Could not unwrap certificate {(certificate != null ? $"\"{certificate.Subject}\"" : "NULL")}.", exception);
			}
		}

		public virtual IValidationResult Validate(ICertificate certificate, CertificateValidatorOptions options)
		{
			return this.Validate(this.UnwrapCertificate(certificate), options);
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual IValidationResult Validate(X509Certificate2 certificate, CertificateValidatorOptions options)
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
					var chain = this.CreateChain(certificate, isSelfSignedCertificate ? options.SelfSigned : options.Chained);

					//if (isSelfSignedCertificate)
					//	chain.ChainPolicy.ExtraStore.Add(certificate);

					if(!chain.Build(certificate))
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
				}
			}
			// ReSharper restore InvertIf

			return validationResult;
		}

		public virtual async Task<IValidationResult> ValidateAsync(ICertificate certificate, CertificateValidatorOptions options)
		{
			// ReSharper disable MethodHasAsyncOverload
			return await Task.FromResult(this.Validate(this.UnwrapCertificate(certificate), options)).ConfigureAwait(false);
			// ReSharper restore MethodHasAsyncOverload
		}

		public virtual async Task<IValidationResult> ValidateAsync(X509Certificate2 certificate, CertificateValidatorOptions options)
		{
			// ReSharper disable MethodHasAsyncOverload
			return await Task.FromResult(this.Validate(certificate, options)).ConfigureAwait(false);
			// ReSharper restore MethodHasAsyncOverload
		}

		#endregion
	}
}