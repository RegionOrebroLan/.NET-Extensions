using System.Collections.Generic;

namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public class MatchingOptions
	{
		#region Properties

		/// <summary>
		/// If all criteria should match or not. Default is false. If true an "and"-compare will be made otherwise an "or"-compare will be made.
		/// </summary>
		public virtual bool AllCriteriaShouldMatch { get; set; }

		/// <summary>
		/// Matching-criteria. The property-name is the name of the certificate-property to match. The value-pattern is matched with "like", so wildcards (*) can be used.
		/// </summary>
		public virtual IList<MatchingCriterionOptions> Criteria { get; } = new List<MatchingCriterionOptions>();

		#endregion
	}
}