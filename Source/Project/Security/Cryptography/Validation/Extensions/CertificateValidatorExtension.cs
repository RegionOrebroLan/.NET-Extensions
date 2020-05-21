using System;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Security.Cryptography.Validation.Configuration;
using RegionOrebroLan.Validation;

namespace RegionOrebroLan.Security.Cryptography.Validation.Extensions
{
	public static class CertificateValidatorExtension
	{
		#region Methods

		public static IValidationResult Validate(this ICertificateValidator certificateValidator, ICertificate certificate, CertificateValidatorOptions options)
		{
			if(certificateValidator == null)
				throw new ArgumentNullException(nameof(certificateValidator));

			return certificateValidator.ValidateAsync(certificate, options).Result;
		}

		public static IValidationResult Validate(this ICertificateValidator certificateValidator, X509Certificate2 certificate, CertificateValidatorOptions options)
		{
			if(certificateValidator == null)
				throw new ArgumentNullException(nameof(certificateValidator));

			return certificateValidator.ValidateAsync(certificate, options).Result;
		}

		#endregion
	}
}