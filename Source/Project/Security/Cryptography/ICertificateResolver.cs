using System.Threading.Tasks;
using RegionOrebroLan.Security.Cryptography.Configuration;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface ICertificateResolver
	{
		#region Methods

		Task<ICertificate> ResolveAsync(ResolverOptions options);

		#endregion
	}
}