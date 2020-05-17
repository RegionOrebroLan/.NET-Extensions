using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.Security.Cryptography.Validation;
using RegionOrebroLan.Security.Cryptography.Validation.Configuration;

namespace RegionOrebroLan.UnitTests.Security.Cryptography.Validation
{
	[TestClass]
	public class CertificateValidatorTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Validate_IfTheCertificateParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new CertificateValidator().Validate((X509Certificate2) null, Mock.Of<CertificateValidatorOptions>());
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(string.Equals(argumentNullException.ParamName, "certificate", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public void Validate_IfTheOptionsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			// ReSharper disable ConvertToUsingDeclaration
			using(var certificate = new X509Certificate2())
			{
				try
				{
					new CertificateValidator().Validate(certificate, null);
				}
				catch(ArgumentNullException argumentNullException)
				{
					if(string.Equals(argumentNullException.ParamName, "options", StringComparison.Ordinal))
						throw;
				}
			}
			// ReSharper restore ConvertToUsingDeclaration
		}

		#endregion
	}
}