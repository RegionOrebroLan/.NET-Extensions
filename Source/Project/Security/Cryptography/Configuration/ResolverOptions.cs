using System;
using System.Threading.Tasks;

namespace RegionOrebroLan.Security.Cryptography.Configuration
{
	public abstract class ResolverOptions
	{
		#region Methods

		public virtual async Task<ICertificate> ResolveAsync(ICertificateResolver certificateResolver)
		{
			return await (certificateResolver ?? throw new ArgumentNullException(nameof(certificateResolver))).ResolveAsync(this).ConfigureAwait(false);
		}

		#endregion
	}
}