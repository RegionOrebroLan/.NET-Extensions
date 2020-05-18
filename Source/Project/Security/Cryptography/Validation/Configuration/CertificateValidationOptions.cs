using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Configuration;

namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public class CertificateValidationOptions
	{
		#region Properties

		/// <summary>
		/// 1.3.6.1.5.5.7.3.2, https://oidref.com/1.3.6.1.5.5.7.3.2, is added by default.
		/// </summary>
		public virtual IList<string> ApplicationPolicyObjectIdentifiers { get; } = new List<string> {"1.3.6.1.5.5.7.3.2"};

		public virtual IList<string> CertificatePolicyObjectIdentifiers { get; } = new List<string>();
		public virtual MatchingOptions Matching { get; set; } = new MatchingOptions();
		public virtual X509RevocationFlag? RevocationFlag { get; set; }
		public virtual X509RevocationMode? RevocationMode { get; set; }
		public virtual IList<DynamicOptions> TrustedIntermediateCertificateResolvers { get; } = new List<DynamicOptions>();
		public virtual IList<ICertificate> TrustedIntermediateCertificates { get; } = new List<ICertificate>();
		public virtual IList<DynamicOptions> TrustedRootCertificateResolvers { get; } = new List<DynamicOptions>();
		public virtual IList<ICertificate> TrustedRootCertificates { get; } = new List<ICertificate>();
		public virtual TimeSpan? UrlRetrievalTimeout { get; set; }
		public virtual bool? UseMachineContext { get; set; }
		public virtual X509VerificationFlags? VerificationFlags { get; set; }
		public virtual DateTime? VerificationTime { get; set; }

		#endregion
	}
}