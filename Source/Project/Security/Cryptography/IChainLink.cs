using System;
using System.Collections.Generic;

namespace RegionOrebroLan.Security.Cryptography
{
	[Obsolete("Overkill. This will be removed.")]
	public interface IChainLink
	{
		#region Properties

		ICertificate Certificate { get; }
		string Information { get; }
		IEnumerable<IChainStatus> Status { get; }

		#endregion
	}
}