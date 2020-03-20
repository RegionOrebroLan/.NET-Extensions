using RegionOrebroLan.Security.Cryptography.Configuration;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface ICertificateResolver
	{
		#region Methods

		ICertificate Resolve(FileResolverOptions options);
		ICertificate Resolve(StoreResolverOptions options);

		#endregion
	}
}