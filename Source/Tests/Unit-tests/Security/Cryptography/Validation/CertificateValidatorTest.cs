using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
		public void ValidateAsync_IfTheCertificateParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				_ = new CertificateValidator().ValidateAsync((X509Certificate2) null, Mock.Of<CertificateValidatorOptions>()).Result;
			}
			catch(AggregateException aggregateException)
			{
				if(aggregateException.InnerExceptions.FirstOrDefault() is ArgumentNullException argumentNullException)
				{
					if(string.Equals(argumentNullException.ParamName, "certificate", StringComparison.Ordinal))
						throw argumentNullException;
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		public void ValidateAsync_IfTheOptionsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			// ReSharper disable ConvertToUsingDeclaration
			using(var certificate = new X509Certificate2())
			{
				try
				{
					_ = new CertificateValidator().ValidateAsync(certificate, null).Result;
				}
				catch(AggregateException aggregateException)
				{
					if(aggregateException.InnerExceptions.FirstOrDefault() is ArgumentNullException argumentNullException)
					{
						if(string.Equals(argumentNullException.ParamName, "options", StringComparison.Ordinal))
							throw argumentNullException;
					}
				}
			}
			// ReSharper restore ConvertToUsingDeclaration
		}

		#endregion
	}
}