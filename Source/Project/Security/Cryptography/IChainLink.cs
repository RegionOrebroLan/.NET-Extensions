using System.Collections.Generic;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface IChainLink
	{
		#region Properties

		ICertificate Certificate { get; }
		string Information { get; }
		IEnumerable<IChainStatus> Status { get; }

		#endregion
	}
}