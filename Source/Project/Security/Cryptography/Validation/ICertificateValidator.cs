using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RegionOrebroLan.Security.Cryptography.Validation.Configuration;
using RegionOrebroLan.Validation;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public interface ICertificateValidator
	{
		#region Methods

		Task<IValidationResult> ValidateAsync(ICertificate certificate, CertificateValidatorOptions options);
		Task<IValidationResult> ValidateAsync(X509Certificate2 certificate, CertificateValidatorOptions options);

		#endregion
	}
}