using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RegionOrebroLan.Configuration;
using RegionOrebroLan.Security.Cryptography.Configuration;

namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public class PostConfigureCertificateValidatorOptions : IPostConfigureOptions<CertificateValidatorOptions>
	{
		#region Constructors

		public PostConfigureCertificateValidatorOptions(ICertificateResolver certificateResolver)
		{
			this.CertificateResolver = certificateResolver ?? throw new ArgumentNullException(nameof(certificateResolver));
		}

		#endregion

		#region Properties

		protected internal virtual ICertificateResolver CertificateResolver { get; }

		#endregion

		#region Methods

		protected internal virtual ResolverOptions CreateResolverOptions(DynamicOptions dynamicOptions)
		{
			if(dynamicOptions == null)
				throw new ArgumentNullException(nameof(dynamicOptions));

			var resolverOptions = (ResolverOptions) Activator.CreateInstance(Type.GetType(dynamicOptions.Type, true, true));

			dynamicOptions.Options?.Bind(resolverOptions);

			return resolverOptions;
		}

		protected internal virtual void PostConfigure(CertificateValidationOptions options)
		{
			if(options == null)
				return;

			foreach(var trustedCertificateResolverOptions in options.TrustedIntermediateCertificateResolvers ?? Enumerable.Empty<DynamicOptions>())
			{
				var resolverOptions = this.CreateResolverOptions(trustedCertificateResolverOptions);
				options.TrustedIntermediateCertificates.Add(resolverOptions.Resolve(this.CertificateResolver));
			}

			foreach(var trustedCertificateResolverOptions in options.TrustedRootCertificateResolvers ?? Enumerable.Empty<DynamicOptions>())
			{
				var resolverOptions = this.CreateResolverOptions(trustedCertificateResolverOptions);
				options.TrustedRootCertificates.Add(resolverOptions.Resolve(this.CertificateResolver));
			}
		}

		public virtual void PostConfigure(string name, CertificateValidatorOptions options)
		{
			this.PostConfigure(options?.Chained);
			this.PostConfigure(options?.SelfSigned);
		}

		#endregion
	}
}