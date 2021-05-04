using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Extensions;
using RegionOrebroLan.IO.Extensions;

namespace RegionOrebroLan.UnitTests.IO.Extensions
{
	[TestClass]
	public class StringExtensionTest
	{
		#region Methods

		[TestMethod]
		public async Task ResolveDataDirectorySubstitution_IfThePathDoesNotStartWithADataDirectorySubstitution_Test()
		{
			await Task.CompletedTask;

			Assert.IsNull(((string) null).ResolveDataDirectorySubstitution());

			foreach(var path in new[] {string.Empty, "    ", "Test", " |DataDirectory|", "Test |DataDirectory| Test"})
			{
				Assert.AreEqual(path, path.ResolveDataDirectorySubstitution());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task ResolveDataDirectorySubstitution_IfThePathStartWithADataDirectorySubstitutionAndTheDataDirectoryIsNotSet_ShouldThrowAnInvalidOperationException()
		{
			await Task.CompletedTask;

			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, null);

			try
			{
				var _ = "|DataDirectory|Test.db".ResolveDataDirectorySubstitution();
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(invalidOperationException.Message.Equals("The data-directory is not set for the application-domain.", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public async Task ResolveDataDirectorySubstitution_IfThePathStartWithADataDirectorySubstitutionAndTheDataDirectoryIsSet_ShouldReturnAStringWithTheSubstitutionReplaced()
		{
			await Task.CompletedTask;

			var dataDirectory = AppDomain.CurrentDomain.GetData(AppDomainExtension.DataDirectoryName);
			AppDomain.CurrentDomain.SetDataDirectory(@"C:\Directory-1");

			const string expectedValue = @"C:\Directory-1\Test.db";
			Assert.AreEqual(expectedValue, "|DataDirectory|Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|DataDirectory|\\Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|DataDirectory|/Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|DATADIRECTORY|Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|DATADIRECTORY|\\Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|DATADIRECTORY|/Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|datadirectory|Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|datadirectory|\\Test.db".ResolveDataDirectorySubstitution());
			Assert.AreEqual(expectedValue, "|datadirectory|/Test.db".ResolveDataDirectorySubstitution());

			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, dataDirectory);
		}

		#endregion
	}
}