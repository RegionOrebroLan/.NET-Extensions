using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Abstractions;
using RegionOrebroLan.Collections.Generic.Extensions;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public class CertificateValidator : ICertificateValidator
	{
		#region Methods

		protected internal virtual X509Chain CreateChain(ICertificateValidationOptions options)
		{
			var useMachineContext = options?.UseMachineContext;

			var chain = useMachineContext == null ? new X509Chain() : new X509Chain(useMachineContext.Value);

			chain.ChainPolicy = this.CreateChainPolicy(options);

			return chain;
		}

		protected internal virtual X509ChainPolicy CreateChainPolicy(ICertificateValidationOptions options)
		{
			var chainPolicy = new X509ChainPolicy();

			// ReSharper disable InvertIf
			if(options != null)
			{
				if(options.ApplicationPolicy.Any())
				{
					foreach(var item in this.GetValidatedObjectIdentifiers(options.ApplicationPolicy, "Application-policy option"))
					{
						chainPolicy.ApplicationPolicy.Add(item);
					}
				}

				if(options.CertificatePolicy.Any())
				{
					foreach(var item in this.GetValidatedObjectIdentifiers(options.CertificatePolicy, "Certificate-policy option"))
					{
						chainPolicy.CertificatePolicy.Add(item);
					}
				}

				if(options.RevocationFlag != null)
					chainPolicy.RevocationFlag = options.RevocationFlag.Value;

				if(options.RevocationMode != null)
					chainPolicy.RevocationMode = options.RevocationMode.Value;

				if(options.UrlRetrievalTimeout != null)
					chainPolicy.UrlRetrievalTimeout = options.UrlRetrievalTimeout.Value;

				if(options.ValidCertificates.Any())
				{
					foreach(var item in this.GetValidatedCertificates(options.ValidCertificates, "Valid certificates option"))
					{
						chainPolicy.ExtraStore.Add(item);
					}
				}

				if(options.VerificationFlags != null)
					chainPolicy.VerificationFlags = options.VerificationFlags.Value;

				if(options.VerificationTime != null)
					chainPolicy.VerificationTime = options.VerificationTime.Value;
			}
			// ReSharper restore InvertIf

			return chainPolicy;
		}

		protected internal virtual Exception CreateInvalidItemTypeException(string label, Type type)
		{
			return new InvalidOperationException($"{label}: the current implementation only supports items implementing \"{type}\".");
		}

		protected internal virtual Exception CreateNullItemException(string label)
		{
			return new InvalidOperationException($"{label}: an item can not be null.");
		}

		protected internal virtual IEnumerable<X509Certificate2> GetValidatedCertificates(IEnumerable<ICertificate> certificates, string label)
		{
			var validatedCertificates = new List<X509Certificate2>();

			foreach(var certificate in certificates ?? Enumerable.Empty<ICertificate>())
			{
				if(certificate == null)
					throw this.CreateNullItemException(label);

				if(!(certificate is IWrapper<X509Certificate2> wrapper))
					throw this.CreateInvalidItemTypeException(label, typeof(IWrapper<X509Certificate2>));

				validatedCertificates.Add(wrapper.WrappedInstance);
			}

			return validatedCertificates;
		}

		protected internal virtual IEnumerable<Oid> GetValidatedObjectIdentifiers(IEnumerable<IOid> items, string label)
		{
			var validatedObjectIdentifiers = new List<Oid>();

			foreach(var item in items ?? Enumerable.Empty<IOid>())
			{
				if(item == null)
					throw this.CreateNullItemException(label);

				if(!(item is IWrapper<Oid> wrapper))
					throw this.CreateInvalidItemTypeException(label, typeof(IWrapper<Oid>));

				validatedObjectIdentifiers.Add(wrapper.WrappedInstance);
			}

			return validatedObjectIdentifiers;
		}

		[SuppressMessage("Design", "CA1031:Do not catch general exception types")]
		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		[SuppressMessage("Maintainability", "CA1506:Avoid excessive class coupling")]
		public virtual ICertificateValidationResult Validate(ICertificate certificate, ICertificateValidationOptions options = null)
		{
			var result = new CertificateValidationResult();

			try
			{
				if(certificate == null)
					throw new ArgumentNullException(nameof(certificate));

				if(!(certificate is IWrapper<X509Certificate2> wrapper))
					throw new ArgumentException($"The current implementation only supports certificates implementing \"{typeof(IWrapper<X509Certificate2>)}\".", nameof(certificate));

				var chain = this.CreateChain(options);

				var valid = chain.Build(wrapper.WrappedInstance);

				result.Chain.Add(chain.ChainElements.Cast<X509ChainElement>().Select(item => (X509ChainElementWrapper) item));
				result.Status.Add(chain.ChainStatus.Select(item => (X509ChainStatusWrapper) item));

				if(!valid)
				{
					if(chain.ChainStatus.Any())
						result.Exceptions.Add(chain.ChainStatus.Select(item => new InvalidOperationException($"{item.Status}: {item.StatusInformation}")));
					else
						result.Exceptions.Add(new InvalidOperationException("The certificate chain is not valid."));
				}
			}
			catch(Exception exception)
			{
				result.Exceptions.Add(exception);
			}

			return result;
		}

		#endregion
	}
}