using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public class CertificateValidatorOptions
	{
		#region Properties

		public virtual CertificateKinds AllowedCertificateKinds { get; set; } = CertificateKinds.Chained;
		public virtual CertificateValidationOptions Chained { get; set; } = new CertificateValidationOptions();

		public virtual CertificateValidationOptions SelfSigned { get; set; } = new CertificateValidationOptions
		{
			RevocationFlag = X509RevocationFlag.EntireChain,
			RevocationMode = X509RevocationMode.NoCheck,
			VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority | X509VerificationFlags.IgnoreEndRevocationUnknown
		};

		#endregion
	}
}