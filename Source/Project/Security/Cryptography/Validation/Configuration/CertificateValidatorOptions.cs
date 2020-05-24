namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public class CertificateValidatorOptions
	{
		#region Properties

		public virtual CertificateKinds AllowedCertificateKinds { get; set; } = CertificateKinds.Chained;
		public virtual CertificateValidationOptions Chained { get; set; } = new CertificateValidationOptions();
		public virtual CertificateValidationOptions SelfSigned { get; set; } = new CertificateValidationOptions();

		#endregion
	}
}