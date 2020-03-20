using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface IChainStatus
	{
		#region Properties

		string Information { get; }
		X509ChainStatusFlags Kind { get; }

		#endregion
	}
}