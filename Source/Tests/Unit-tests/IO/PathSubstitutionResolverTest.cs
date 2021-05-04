using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Extensions;
using RegionOrebroLan.IO;

namespace RegionOrebroLan.UnitTests.IO
{
	[TestClass]
	public class PathSubstitutionResolverTest
	{
		#region Methods

		protected internal virtual async Task<IApplicationDomain> CreateApplicationDomainAsync()
		{
			return await Task.FromResult(new AppDomainWrapper(AppDomain.CurrentDomain));
		}

		protected internal virtual async Task<PathSubstitutionResolver> CreatePathSubstitutionResolverAsync()
		{
			return new PathSubstitutionResolver(await this.CreateApplicationDomainAsync());
		}

		protected internal virtual async Task<PathSubstitutionResolver> CreatePathSubstitutionResolverAsync(IApplicationDomain applicationDomain)
		{
			return await Task.FromResult(new PathSubstitutionResolver(applicationDomain));
		}

		[TestMethod]
		public async Task ResolveDataDirectoryAsync_IfThePathDoesNotStartWithADataDirectorySubstitution_Test()
		{
			var pathSubstitutionResolver = await this.CreatePathSubstitutionResolverAsync();

			Assert.IsNull(await pathSubstitutionResolver.ResolveDataDirectoryAsync(null));

			foreach(var path in new[] {string.Empty, "    ", "Test", " |DataDirectory|", "Test |DataDirectory| Test"})
			{
				Assert.AreEqual(path, await pathSubstitutionResolver.ResolveDataDirectoryAsync(path));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task ResolveDataDirectoryAsync_IfThePathStartWithADataDirectorySubstitutionAndTheDataDirectoryIsNotSet_ShouldThrowAnInvalidOperationException()
		{
			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, null);

			var pathSubstitutionResolver = await this.CreatePathSubstitutionResolverAsync();

			try
			{
				var _ = await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DataDirectory|Test.db");
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(invalidOperationException.Message.Equals("The data-directory is not set for the application-domain.", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public async Task ResolveDataDirectoryAsync_IfThePathStartWithADataDirectorySubstitutionAndTheDataDirectoryIsSet_ShouldReturnAStringWithTheSubstitutionReplaced()
		{
			var dataDirectory = AppDomain.CurrentDomain.GetData(AppDomainExtension.DataDirectoryName);
			AppDomain.CurrentDomain.SetDataDirectory(@"C:\Directory-1");

			var pathSubstitutionResolver = await this.CreatePathSubstitutionResolverAsync();

			const string expectedValue = @"C:\Directory-1\Test.db";
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DataDirectory|Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DataDirectory|\\Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DataDirectory|/Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DATADIRECTORY|Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DATADIRECTORY|\\Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|DATADIRECTORY|/Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|datadirectory|Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|datadirectory|\\Test.db"));
			Assert.AreEqual(expectedValue, await pathSubstitutionResolver.ResolveDataDirectoryAsync("|datadirectory|/Test.db"));

			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, dataDirectory);
		}

		#endregion
	}
}