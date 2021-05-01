using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan.UnitTests.Extensions
{
	[TestClass]
	public class AppDomainExtensionTest
	{
		#region Fields

		private static object _dataDirectory;

		#endregion

		#region Methods

		[ClassCleanup]
		public static async Task CleanupAsync()
		{
			await Task.CompletedTask;

			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, _dataDirectory);
		}

		[TestMethod]
		public async Task GetDataDirectory_IfTheValidateParameterIsSetToFalse_ShouldWorkCorrectly()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;

			appDomain.SetData(AppDomainExtension.DataDirectoryName, null);
			Assert.IsNull(appDomain.GetDataDirectory(false));

			appDomain.SetData(AppDomainExtension.DataDirectoryName, new object());
			Assert.IsNull(appDomain.GetDataDirectory(false));

			appDomain.SetData(AppDomainExtension.DataDirectoryName, "Test");
			Assert.AreEqual("Test", appDomain.GetDataDirectory(false));
		}

		[TestMethod]
		[SuppressMessage("Design", "CA1031:Do not catch general exception types")]
		public async Task GetDataDirectory_IfTheValidateParameterIsSetToTrue_ShouldWorkCorrectly()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;

			appDomain.SetData(AppDomainExtension.DataDirectoryName, null);
			Assert.IsNull(appDomain.GetDataDirectory(true));

			appDomain.SetData(AppDomainExtension.DataDirectoryName, new object());
			try
			{
				Assert.IsNull(appDomain.GetDataDirectory(true));
				Assert.Fail("Should have thrown an exception.");
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch { }
			// ReSharper restore EmptyGeneralCatchClause

			appDomain.SetData(AppDomainExtension.DataDirectoryName, "Test");
			Assert.AreEqual("Test", appDomain.GetDataDirectory(true));
		}

		[TestMethod]
		[SuppressMessage("Design", "CA1031:Do not catch general exception types")]
		public async Task GetDataDirectory_ShouldWorkCorrectly()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;

			appDomain.SetData(AppDomainExtension.DataDirectoryName, null);
			Assert.IsNull(appDomain.GetDataDirectory());

			appDomain.SetData(AppDomainExtension.DataDirectoryName, new object());
			try
			{
				Assert.IsNull(appDomain.GetDataDirectory());
				Assert.Fail("Should have thrown an exception.");
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch { }
			// ReSharper restore EmptyGeneralCatchClause

			appDomain.SetData(AppDomainExtension.DataDirectoryName, "Test");
			Assert.AreEqual("Test", appDomain.GetDataDirectory());
		}

		[ClassInitialize]
		public static async Task InitializeAsync(TestContext _)
		{
			await Task.CompletedTask;

			_dataDirectory = AppDomain.CurrentDomain.GetData(AppDomainExtension.DataDirectoryName);
		}

		[TestMethod]
		public async Task RemoveDataDirectory_IfDataDirectoryIsNotSet_ShouldReturnFalse()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;
			appDomain.SetData(AppDomainExtension.DataDirectoryName, null);
			Assert.IsFalse(appDomain.RemoveDataDirectory());
		}

		[TestMethod]
		public async Task RemoveDataDirectory_IfDataDirectoryIsSet_ShouldRemoveDataDirectoryAndReturnTrue()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;

			var dataDirectory = new object();
			appDomain.SetData(AppDomainExtension.DataDirectoryName, dataDirectory);
			Assert.IsNotNull(appDomain.GetData(AppDomainExtension.DataDirectoryName));
			Assert.AreEqual(dataDirectory, appDomain.GetData(AppDomainExtension.DataDirectoryName));
			Assert.IsTrue(appDomain.RemoveDataDirectory());
			Assert.IsNull(appDomain.GetData(AppDomainExtension.DataDirectoryName));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task SetDataDirectory_IfThePathIsSetToAnEmptyStringAndIfTheValidateParameterIsSetToTrue_ShouldThrowAnArgumentException()
		{
			await Task.CompletedTask;

			AppDomain.CurrentDomain.SetDataDirectory(string.Empty, true);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task SetDataDirectory_IfThePathIsSetToANonExistingDirectoryStringAndIfTheValidateParameterIsSetToTrue_ShouldThrowAnArgumentException()
		{
			await Task.CompletedTask;

			const string path = @"C:\eb55e5fb-d63c-4824-befa-431ca0a560c8";

			try
			{
				AppDomain.CurrentDomain.SetDataDirectory(path, true);
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.InnerException is DirectoryNotFoundException)
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task SetDataDirectory_IfThePathIsSetToNullAndIfTheValidateParameterIsSetToTrue_ShouldThrowAnArgumentNullException()
		{
			await Task.CompletedTask;

			AppDomain.CurrentDomain.SetDataDirectory(null, true);
		}

		[TestMethod]
		public async Task SetDataDirectory_IfTheValidateParameterIsSetToFalse_ShouldWorkCorrectly()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;

			appDomain.SetDataDirectory(null, false);
			Assert.IsNull(appDomain.GetData(AppDomainExtension.DataDirectoryName));

			var path = string.Empty;
			appDomain.SetDataDirectory(path, false);
			Assert.AreEqual(path, appDomain.GetData(AppDomainExtension.DataDirectoryName));

			path = "    ";
			appDomain.SetDataDirectory(path, false);
			Assert.AreEqual(path, appDomain.GetData(AppDomainExtension.DataDirectoryName));

			path = "Test";
			appDomain.SetDataDirectory(path, false);
			Assert.AreEqual(path, appDomain.GetData(AppDomainExtension.DataDirectoryName));
		}

		[TestMethod]
		public async Task SetDataDirectory_ShouldWorkCorrectly()
		{
			await Task.CompletedTask;

			var appDomain = AppDomain.CurrentDomain;

			appDomain.SetDataDirectory(null);
			Assert.IsNull(appDomain.GetData(AppDomainExtension.DataDirectoryName));

			var path = string.Empty;
			appDomain.SetDataDirectory(path);
			Assert.AreEqual(path, appDomain.GetData(AppDomainExtension.DataDirectoryName));

			path = "    ";
			appDomain.SetDataDirectory(path);
			Assert.AreEqual(path, appDomain.GetData(AppDomainExtension.DataDirectoryName));

			path = "Test";
			appDomain.SetDataDirectory(path);
			Assert.AreEqual(path, appDomain.GetData(AppDomainExtension.DataDirectoryName));
		}

		#endregion
	}
}