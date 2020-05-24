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
	// ReSharper disable ConvertToUsingDeclaration
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

		protected internal virtual X509Certificate2 GetFirstValidCertificateFromLocalMachineRootStore()
		{
			using(var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
			{
				store.Open(OpenFlags.ReadOnly);

				var trustedRootCertificate = store.Certificates.Cast<X509Certificate2>().FirstOrDefault(certificate => certificate.Verify());

				Assert.IsNotNull(trustedRootCertificate, "The prerequisites for the test are not met. There is no valid certificate in the local-machine root store.");

				return trustedRootCertificate;
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(6, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(1, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
				{
					var options = new CertificateValidatorOptions
					{
						AllowedCertificateKinds = CertificateKinds.All,
						Chained =
						{
							ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
							RevocationMode = X509RevocationMode.NoCheck,
							VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
						}
					};
					options.Chained.TrustedRootCertificates.Add(root);
					var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
					Assert.IsTrue(validationResult.Valid);
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(6, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(1, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
				{
					var options = new CertificateValidatorOptions
					{
						AllowedCertificateKinds = CertificateKinds.Chained,
						Chained =
						{
							ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
							RevocationMode = X509RevocationMode.NoCheck,
							VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
						}
					};
					options.Chained.TrustedRootCertificates.Add(root);
					var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
					Assert.IsTrue(validationResult.Valid);
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained_IfChainedAllowedOnly_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.ChainedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(6, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(8, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(2, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var intermediate = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate1Path))
				{
					using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
					{
						var options = new CertificateValidatorOptions
						{
							AllowedCertificateKinds = CertificateKinds.All,
							Chained =
							{
								ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
								RevocationMode = X509RevocationMode.NoCheck,
								VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
							}
						};
						options.Chained.TrustedIntermediateCertificates.Add(intermediate);
						options.Chained.TrustedRootCertificates.Add(root);
						var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
						Assert.IsTrue(validationResult.Valid);
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(8, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(2, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var intermediate = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate1Path))
				{
					using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
					{
						var options = new CertificateValidatorOptions
						{
							AllowedCertificateKinds = CertificateKinds.Chained,
							Chained =
							{
								ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
								RevocationMode = X509RevocationMode.NoCheck,
								VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
							}
						};
						options.Chained.TrustedIntermediateCertificates.Add(intermediate);
						options.Chained.TrustedRootCertificates.Add(root);
						var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
						Assert.IsTrue(validationResult.Valid);
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained1_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained1Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(10, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(3, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var intermediate1 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate1Path))
				{
					using(var intermediate2 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate2Path))
					{
						using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
						{
							var options = new CertificateValidatorOptions
							{
								AllowedCertificateKinds = CertificateKinds.All,
								Chained =
								{
									ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
									RevocationMode = X509RevocationMode.NoCheck,
									VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
								}
							};
							options.Chained.TrustedIntermediateCertificates.Add(intermediate1);
							options.Chained.TrustedIntermediateCertificates.Add(intermediate2);
							options.Chained.TrustedRootCertificates.Add(root);
							var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
							Assert.IsTrue(validationResult.Valid);
						}
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(10, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(3, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var intermediate1 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate1Path))
				{
					using(var intermediate2 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate2Path))
					{
						using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
						{
							var options = new CertificateValidatorOptions
							{
								AllowedCertificateKinds = CertificateKinds.Chained,
								Chained =
								{
									ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
									RevocationMode = X509RevocationMode.NoCheck,
									VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
								}
							};
							options.Chained.TrustedIntermediateCertificates.Add(intermediate1);
							options.Chained.TrustedIntermediateCertificates.Add(intermediate2);
							options.Chained.TrustedRootCertificates.Add(root);
							var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
							Assert.IsTrue(validationResult.Valid);
						}
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained2_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained2Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(12, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(4, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var intermediate1 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate1Path))
				{
					using(var intermediate2 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate2Path))
					{
						using(var intermediate3 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate3Path))
						{
							using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
							{
								var options = new CertificateValidatorOptions
								{
									AllowedCertificateKinds = CertificateKinds.All,
									Chained =
									{
										ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
										RevocationMode = X509RevocationMode.NoCheck,
										VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
									}
								};
								options.Chained.TrustedIntermediateCertificates.Add(intermediate1);
								options.Chained.TrustedIntermediateCertificates.Add(intermediate2);
								options.Chained.TrustedIntermediateCertificates.Add(intermediate3);
								options.Chained.TrustedRootCertificates.Add(root);
								var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
								Assert.IsTrue(validationResult.Valid);
							}
						}
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfSystemTrust_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfNotRevocationNoCheck_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(12, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
				Assert.AreEqual(4, validationResult.Exceptions.Count);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var root = (X509Certificate2Wrapper) new X509Certificate2(this.RootPath))
			{
				using(var intermediate1 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate1Path))
				{
					using(var intermediate2 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate2Path))
					{
						using(var intermediate3 = (X509Certificate2Wrapper) new X509Certificate2(this.Intermediate3Path))
						{
							using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
							{
								var options = new CertificateValidatorOptions
								{
									AllowedCertificateKinds = CertificateKinds.Chained,
									Chained =
									{
										ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
										RevocationMode = X509RevocationMode.NoCheck,
										VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
									}
								};
								options.Chained.TrustedIntermediateCertificates.Add(intermediate1);
								options.Chained.TrustedIntermediateCertificates.Add(intermediate2);
								options.Chained.TrustedIntermediateCertificates.Add(intermediate3);
								options.Chained.TrustedRootCertificates.Add(root);
								var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
								Assert.IsTrue(validationResult.Valid);
							}
						}
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_And_IfSystemTrust_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileChained3_IfChainedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfRevocationNoCheck_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.Chained3Path))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.Chained,
					Chained =
					{
						RevocationMode = X509RevocationMode.NoCheck,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					SelfSigned =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					SelfSigned =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				options.SelfSigned.TrustedRootCertificates.Add(certificate);
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfAllAllowed_And_IfAllowUnknownCertificateAuthority_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All,
					SelfSigned =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfAllAllowedOnly_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.All
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfDefaultOptions_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions();
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfSelfSignedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfCustomRootTrust_And_IfNotTrustedList_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.SelfSigned,
					SelfSigned =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfSelfSignedAllowed_And_IfAllowUnknownCertificateAuthority_And_IfCustomRootTrust_And_IfTrustedList_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.SelfSigned,
					SelfSigned =
					{
						ChainTrustMode = X509ChainTrustMode.CustomRootTrust,
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				options.SelfSigned.TrustedRootCertificates.Add(certificate);
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfSelfSignedAllowed_And_IfAllowUnknownCertificateAuthority_ShouldSucceed()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.SelfSigned,
					SelfSigned =
					{
						VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
					}
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_FileSelfSigned_IfSelfSignedAllowedOnly_ShouldFail()
		{
			using(var certificate = (X509Certificate2Wrapper) new X509Certificate2(this.SelfSignedPath))
			{
				var options = new CertificateValidatorOptions
				{
					AllowedCertificateKinds = CertificateKinds.SelfSigned
				};
				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_MatchingForChainedCertificate_Test()
		{
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
								var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
								Assert.IsTrue(validationResult.Valid);

								// Issuer matching exact
								Assert.AreEqual("CN=Intermediate-3", certificate.Issuer);
								options.Chained.Matching.Criteria.Add(new MatchingCriterionOptions
								{
									PropertyName = "Issuer",
									ValuePattern = certificate.Issuer
								});
								Assert.IsFalse(options.Chained.Matching.AllCriteriaShouldMatch);
								validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
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
								validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
								Assert.IsTrue(validationResult.Valid);

								options.Chained.Matching.AllCriteriaShouldMatch = true;
								validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
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
								validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;
								Assert.IsTrue(validationResult.Valid);
							}
						}
					}
				}
			}
		}

		[TestMethod]
		public void ValidateAsync_StoreRoot_IfAllAllowed_ShouldSucceed()
		{
			using(var certificate = this.GetFirstValidCertificateFromLocalMachineRootStore())
			{
				var options = new CertificateValidatorOptions {AllowedCertificateKinds = CertificateKinds.All};

				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;

				Assert.IsTrue(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_StoreRoot_IfChainedAllowed_ShouldFail()
		{
			using(var certificate = this.GetFirstValidCertificateFromLocalMachineRootStore())
			{
				var options = new CertificateValidatorOptions {AllowedCertificateKinds = CertificateKinds.Chained};

				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;

				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_StoreRoot_IfDefaultOptions_ShouldFail()
		{
			using(var certificate = this.GetFirstValidCertificateFromLocalMachineRootStore())
			{
				var options = new CertificateValidatorOptions();

				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;

				Assert.IsFalse(validationResult.Valid);
			}
		}

		[TestMethod]
		public void ValidateAsync_StoreRoot_IfSelfSignedAllowed_ShouldSucceed()
		{
			using(var certificate = this.GetFirstValidCertificateFromLocalMachineRootStore())
			{
				var options = new CertificateValidatorOptions {AllowedCertificateKinds = CertificateKinds.SelfSigned};

				var validationResult = new CertificateValidator().ValidateAsync(certificate, options).Result;

				Assert.IsTrue(validationResult.Valid);
			}
		}

		#endregion
	}
	// ReSharper restore ConvertToUsingDeclaration
}