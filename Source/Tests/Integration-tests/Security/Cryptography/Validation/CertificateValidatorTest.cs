using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Security.Cryptography;
using RegionOrebroLan.Security.Cryptography.Validation;
using RegionOrebroLan.Security.Cryptography.Validation.Configuration;

namespace RegionOrebroLan.IntegrationTests.Security.Cryptography.Validation
{
	[TestClass]
	public class CertificateValidatorTest
	{
		#region Fields

		private static readonly string _certificateDirectoryPath = Path.Combine(Global.ProjectDirectoryPath, "Security", "Cryptography", "Validation", "Resources", "Certificates");

		#endregion

		#region Properties

		protected internal virtual string CertificateDirectoryPath => _certificateDirectoryPath;
		protected internal virtual string Chained1Path => Path.Combine(this.CertificateDirectoryPath, "Chained-1.cer");
		protected internal virtual string Chained2Path => Path.Combine(this.CertificateDirectoryPath, "Chained-2.cer");
		protected internal virtual string Chained3Path => Path.Combine(this.CertificateDirectoryPath, "Chained-3.cer");
		protected internal virtual string ChainedPath => Path.Combine(this.CertificateDirectoryPath, "Chained.cer");
		protected internal virtual string Intermediate1Path => Path.Combine(this.CertificateDirectoryPath, "Intermediate-1.cer");
		protected internal virtual string Intermediate2Path => Path.Combine(this.CertificateDirectoryPath, "Intermediate-2.cer");
		protected internal virtual string Intermediate3Path => Path.Combine(this.CertificateDirectoryPath, "Intermediate-3.cer");
		protected internal virtual string RootPath => Path.Combine(this.CertificateDirectoryPath, "Root.cer");
		protected internal virtual string SelfSignedPath => Path.Combine(this.CertificateDirectoryPath, "Self-signed.cer");

		#endregion

		#region Methods

		[TestMethod]
		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public void Validate_IfTheCertificateIsChained_And_IfAllowUnknownCertificateAuthorityIsSet_ShouldReturnAValidValidationResult()
		{
			// ReSharper disable ConvertToUsingDeclaration
			using(var certificate = new X509Certificate2(this.Chained3Path))
			{
				using(var intermediate1 = new X509Certificate2(this.Intermediate1Path))
				{
					using(var intermediate2 = new X509Certificate2(this.Intermediate2Path))
					{
						using(var intermediate3 = new X509Certificate2(this.Intermediate3Path))
						{
							using(var root = new X509Certificate2(this.RootPath))
							{
								var options = new CertificateValidatorOptions
								{
									Chained =
									{
										RevocationFlag = X509RevocationFlag.EntireChain,
										RevocationMode = X509RevocationMode.NoCheck
									}
								};

								// The following add does not seem to be necessary.
								options.Chained.TrustedIntermediateCertificates.Add((X509Certificate2Wrapper) intermediate1);
								options.Chained.TrustedIntermediateCertificates.Add((X509Certificate2Wrapper) intermediate2);
								options.Chained.TrustedIntermediateCertificates.Add((X509Certificate2Wrapper) intermediate3);
								options.Chained.TrustedRootCertificates.Add((X509Certificate2Wrapper) root);

								options.Chained.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;

								var validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsNotNull(validationResult);
								Assert.IsTrue(validationResult.Valid);
								Assert.IsFalse(validationResult.Exceptions.Any());

								options.AllowedCertificateKinds = CertificateKinds.All;
								validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsNotNull(validationResult);
								Assert.IsTrue(validationResult.Valid);
								Assert.IsFalse(validationResult.Exceptions.Any());

								options.AllowedCertificateKinds = CertificateKinds.Chained;
								validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsNotNull(validationResult);
								Assert.IsTrue(validationResult.Valid);
								Assert.IsFalse(validationResult.Exceptions.Any());
							}
						}
					}
				}
			}
			// ReSharper restore ConvertToUsingDeclaration
		}

		[TestMethod]
		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public void Validate_IfTheCertificateIsSelfSigned_And_IfOptionsAllowSelfSigned_ShouldReturnAValidValidationResult()
		{
			// ReSharper disable ConvertToUsingDeclaration
			using(var certificate = new X509Certificate2(this.SelfSignedPath))
			{
				var validationResult = new CertificateValidator().Validate(certificate, new CertificateValidatorOptions {AllowedCertificateKinds = CertificateKinds.All});

				Assert.IsNotNull(validationResult);
				Assert.IsTrue(validationResult.Valid);
				Assert.IsFalse(validationResult.Exceptions.Any());

				validationResult = new CertificateValidator().Validate(certificate, new CertificateValidatorOptions {AllowedCertificateKinds = CertificateKinds.SelfSigned});

				Assert.IsNotNull(validationResult);
				Assert.IsTrue(validationResult.Valid);
				Assert.IsFalse(validationResult.Exceptions.Any());
			}
			// ReSharper restore ConvertToUsingDeclaration
		}

		[TestMethod]
		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public void Validate_MatchingForChainedCertificate_Test()
		{
			// ReSharper disable ConvertToUsingDeclaration
			using(var certificate = new X509Certificate2(this.Chained3Path))
			{
				using(var intermediate1 = new X509Certificate2(this.Intermediate1Path))
				{
					using(var intermediate2 = new X509Certificate2(this.Intermediate2Path))
					{
						using(var intermediate3 = new X509Certificate2(this.Intermediate3Path))
						{
							using(var root = new X509Certificate2(this.RootPath))
							{
								var options = new CertificateValidatorOptions
								{
									AllowedCertificateKinds = CertificateKinds.Chained,
									Chained =
									{
										RevocationFlag = X509RevocationFlag.EntireChain,
										RevocationMode = X509RevocationMode.NoCheck
									}
								};

								// The following add does not seem to be necessary.
								options.Chained.TrustedIntermediateCertificates.Add((X509Certificate2Wrapper) intermediate1);
								options.Chained.TrustedIntermediateCertificates.Add((X509Certificate2Wrapper) intermediate2);
								options.Chained.TrustedIntermediateCertificates.Add((X509Certificate2Wrapper) intermediate3);
								options.Chained.TrustedRootCertificates.Add((X509Certificate2Wrapper) root);

								options.Chained.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;

								// No matching
								var validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsTrue(validationResult.Valid);

								// Issuer matching exact
								Assert.AreEqual("CN=Intermediate-3", certificate.Issuer);
								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "Issuer",
									ValuePattern = certificate.Issuer
								});
								Assert.IsFalse(options.Chained.Matching.AllCriteriaShouldMatch);
								validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsTrue(validationResult.Valid);

								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "issuer",
									ValuePattern = "fd92abb7-3972-429a-90da-cdd1c6df49df"
								});
								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "iSsUeR",
									ValuePattern = "11bd3d77-8313-4c2f-afff-b91f31b26323"
								});
								validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsTrue(validationResult.Valid);

								options.Chained.Matching.AllCriteriaShouldMatch = true;
								validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsFalse(validationResult.Valid);
								Assert.AreEqual(2, validationResult.Exceptions.Count);

								// Issuer matching any
								options.Chained.Matching.AllCriteriaShouldMatch = false;
								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "Issuer",
									ValuePattern = certificate.Issuer
								});
								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "issuer",
									ValuePattern = "fd92abb7-3972-429a-90da-cdd1c6df49df"
								});
								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "iSsUeR",
									ValuePattern = "11bd3d77-8313-4c2f-afff-b91f31b26323"
								});
								validationResult = new CertificateValidator().Validate(certificate, options);
								Assert.IsTrue(validationResult.Valid);
							}
						}
					}
				}
			}
			// ReSharper restore ConvertToUsingDeclaration
		}

		#endregion
	}
}