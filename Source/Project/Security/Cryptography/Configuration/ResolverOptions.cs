using System;
using System.Threading.Tasks;

namespace RegionOrebroLan.Security.Cryptography.Configuration
{
	public abstract class ResolverOptions
	{
		#region Methods

		[Obsolete("Use RegionOrebroLan.Security.Cryptography.ICertificateResolver.ResolveAsync(ResolverOptions options) instead. This method will be removed in some future release.")]
		public virtual async Task<ICertificate> ResolveAsync(ICertificateResolver certificateResolver)
		{
			return await (certificateResolver ?? throw new ArgumentNullException(nameof(certificateResolver))).ResolveAsync(this).ConfigureAwait(false);
		}

		#endregion
	}
}