using System;

namespace RegionOrebroLan.Security.Cryptography.Configuration.Extensions
{
	public static class ResolverOptionsExtension
	{
		#region Methods

		public static ICertificate Resolve(this ResolverOptions options, ICertificateResolver certificateResolver)
		{
			if(options == null)
				throw new ArgumentNullException(nameof(options));

			return options.ResolveAsync(certificateResolver).Result;
		}

		#endregion
	}
}