using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Security.Cryptography;
using RegionOrebroLan.Security.Cryptography.Validation;

namespace RegionOrebroLan.IntegrationTests.Security.Cryptography.Validation
{
	[TestClass]
	public class CertificateValidatorTest
	{
		#region Methods

		[TestMethod]
		public void Test()
		{
			Assert.Inconclusive("Tests needed.");

			//var certificateValidator = new CertificateValidator();

			//using(var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			//{
			//	store.Open(OpenFlags.ReadOnly);

			//	var certificate = (X509Certificate2Wrapper) store.Certificates.Find(X509FindType.FindByThumbprint, "Invalid", false).Cast<X509Certificate2>().First();

			//	var result = certificateValidator.Validate(certificate);
			//}
		}

		#endregion
	}
}