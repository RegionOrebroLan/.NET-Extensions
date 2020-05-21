using System;
using RegionOrebroLan.Security.Cryptography.Configuration;

namespace RegionOrebroLan.Security.Cryptography.Extensions
{
	public static class CertificateResolverExtension
	{
		#region Methods

		public static ICertificate Resolve(this ICertificateResolver certificateResolver, ResolverOptions options)
		{
			if(certificateResolver == null)
				throw new ArgumentNullException(nameof(certificateResolver));

			return certificateResolver.ResolveAsync(options).Result;
		}

		#endregion
	}
}