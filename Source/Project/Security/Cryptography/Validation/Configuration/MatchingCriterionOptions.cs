namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public class MatchingCriterionOptions
	{
		#region Properties

		/// <summary>
		/// A property-name for a certificate, eg. "Issuer", "Subject", "Thumbprint" etc.
		/// </summary>
		public virtual string PropertyName { get; set; }

		/// <summary>
		/// Wildcards, *, can be used.
		/// </summary>
		public virtual string ValuePattern { get; set; }

		#endregion
	}
}