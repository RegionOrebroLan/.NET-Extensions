using System.Collections.Generic;
using RegionOrebroLan.Validation;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public class CertificateValidationResult : ValidationResult, ICertificateValidationResult
	{
		#region Properties

		IEnumerable<IChainLink> ICertificateValidationResult.Chain => this.Chain;
		public virtual IList<IChainLink> Chain { get; } = new List<IChainLink>();
		IEnumerable<IChainStatus> ICertificateValidationResult.Status => this.Status;
		public virtual IList<IChainStatus> Status { get; } = new List<IChainStatus>();

		#endregion
	}
}