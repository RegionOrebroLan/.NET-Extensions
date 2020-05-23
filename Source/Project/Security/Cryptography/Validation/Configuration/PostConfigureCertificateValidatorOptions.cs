using System;
using System.Linq;
using System.Threading.Tasks;
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

		protected internal virtual async Task<ResolverOptions> CreateResolverOptionsAsync(DynamicOptions dynamicOptions)
		{
			if(dynamicOptions == null)
				throw new ArgumentNullException(nameof(dynamicOptions));

			var resolverOptions = (ResolverOptions) Activator.CreateInstance(Type.GetType(dynamicOptions.Type, true, true));

			dynamicOptions.Options?.Bind(resolverOptions);

			return await Task.FromResult(resolverOptions).ConfigureAwait(false);
		}

		public virtual void PostConfigure(string name, CertificateValidatorOptions options)
		{
			this.PostConfigureAsync(options?.Chained).Wait();
			this.PostConfigureAsync(options?.SelfSigned).Wait();
		}

		protected internal virtual async Task PostConfigureAsync(CertificateValidationOptions options)
		{
			if(options == null)
				return;

			foreach(var trustedCertificateResolverOptions in options.TrustedIntermediateCertificateResolvers ?? Enumerable.Empty<DynamicOptions>())
			{
				var resolverOptions = await this.CreateResolverOptionsAsync(trustedCertificateResolverOptions).ConfigureAwait(false);
				options.TrustedIntermediateCertificates.Add(await this.CertificateResolver.ResolveAsync(resolverOptions).ConfigureAwait(false));
			}

			foreach(var trustedCertificateResolverOptions in options.TrustedRootCertificateResolvers ?? Enumerable.Empty<DynamicOptions>())
			{
				var resolverOptions = await this.CreateResolverOptionsAsync(trustedCertificateResolverOptions).ConfigureAwait(false);
				options.TrustedRootCertificates.Add(await this.CertificateResolver.ResolveAsync(resolverOptions).ConfigureAwait(false));
			}
		}

		#endregion
	}
}