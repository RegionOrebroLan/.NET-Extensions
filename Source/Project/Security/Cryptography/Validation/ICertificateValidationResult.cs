using System;
using System.Collections.Generic;
using RegionOrebroLan.Validation;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	[Obsolete("Overkill. This will be removed.")]
	public interface ICertificateValidationResult : IValidationResult
	{
		#region Properties

		IEnumerable<IChainLink> Chain { get; }
		IEnumerable<IChainStatus> Status { get; }

		#endregion
	}
}