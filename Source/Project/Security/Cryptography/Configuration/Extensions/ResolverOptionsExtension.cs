using System;

namespace RegionOrebroLan.Security.Cryptography.Configuration.Extensions
{
	[Obsolete("Use extension RegionOrebroLan.Security.Cryptography.Extensions.CertificateResolverExtension instead. This extension will be removed in some future release.")]
	public static class ResolverOptionsExtension
	{
		#region Methods

		[Obsolete("Use extension RegionOrebroLan.Security.Cryptography.Extensions.CertificateResolverExtension.Resolve(this ICertificateResolver certificateResolver, ResolverOptions options) instead. This extension will be removed in some future release.")]
		public static ICertificate Resolve(this ResolverOptions options, ICertificateResolver certificateResolver)
		{
			if(options == null)
				throw new ArgumentNullException(nameof(options));

			if(certificateResolver == null)
				throw new ArgumentNullException(nameof(certificateResolver));

			return certificateResolver.ResolveAsync(options).Result;
		}

		#endregion
	}
}