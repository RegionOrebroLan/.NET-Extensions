using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public class CertificateValidationOptions : ICertificateValidationOptions
	{
		#region Properties

		public virtual IList<IOid> ApplicationPolicy { get; } = new List<IOid>();
		public virtual IList<IOid> CertificatePolicy { get; } = new List<IOid>();
		public virtual X509RevocationFlag? RevocationFlag { get; set; }
		public virtual X509RevocationMode? RevocationMode { get; set; }
		public virtual TimeSpan? UrlRetrievalTimeout { get; set; }
		public virtual bool? UseMachineContext { get; set; }
		public virtual IList<ICertificate> ValidCertificates { get; } = new List<ICertificate>();
		public virtual X509VerificationFlags? VerificationFlags { get; set; }
		public virtual DateTime? VerificationTime { get; set; }

		#endregion
	}
}