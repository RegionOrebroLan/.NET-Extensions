using System.Collections.Generic;
using RegionOrebroLan.Validation;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public interface ICertificateValidationResult : IValidationResult
	{
		#region Properties

		IEnumerable<IChainLink> Chain { get; }
		IEnumerable<IChainStatus> Status { get; }

		#endregion
	}
}