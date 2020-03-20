using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography.Validation.Configuration
{
	public interface ICertificateValidationOptions
	{
		#region Properties

		IList<IOid> ApplicationPolicy { get; }
		IList<IOid> CertificatePolicy { get; }
		X509RevocationFlag? RevocationFlag { get; }
		X509RevocationMode? RevocationMode { get; }
		TimeSpan? UrlRetrievalTimeout { get; }
		bool? UseMachineContext { get; }
		IList<ICertificate> ValidCertificates { get; }
		X509VerificationFlags? VerificationFlags { get; }
		DateTime? VerificationTime { get; }

		#endregion
	}
}