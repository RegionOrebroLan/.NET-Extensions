using RegionOrebroLan.Security.Cryptography.Validation.Configuration;

namespace RegionOrebroLan.Security.Cryptography.Validation
{
	public interface ICertificateValidator
	{
		#region Methods

		ICertificateValidationResult Validate(ICertificate certificate, ICertificateValidationOptions options = null);

		#endregion
	}
}