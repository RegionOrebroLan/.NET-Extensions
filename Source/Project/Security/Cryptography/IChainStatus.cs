using System;
using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography
{
	[Obsolete("Overkill. This will be removed.")]
	public interface IChainStatus
	{
		#region Properties

		string Information { get; }
		X509ChainStatusFlags Kind { get; }

		#endregion
	}
}