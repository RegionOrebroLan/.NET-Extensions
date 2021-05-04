using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan.IntegrationTests.Extensions
{
	[TestClass]
	public class ApplicationDomainExtensionTest
	{
		#region Methods

		[TestMethod]
		public void GetDataDirectory_IfTheDataDirectoryIsNotSet_ShouldThrowAnInvalidOperationException()
		{
			var dataDirectory = AppDomain.CurrentDomain.GetData(AppDomainExtension.DataDirectoryName);
			var applicationDomain = (AppDomainWrapper) AppDomain.CurrentDomain;
			Assert.IsNull(applicationDomain.GetDataDirectory());
			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, dataDirectory);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetDataDirectory_IfTheDataDirectoryIsNotSetAsString_ShouldThrowAnInvalidOperationException()
		{
			var dataDirectory = AppDomain.CurrentDomain.GetData(AppDomainExtension.DataDirectoryName);
			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, new object());
			var applicationDomain = (AppDomainWrapper) AppDomain.CurrentDomain;

			try
			{
				var _ = applicationDomain.GetDataDirectory();
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(string.Equals(invalidOperationException.Message, "The current value for \"DataDirectory\" is invalid. The value should be of type \"System.String\" but is of type \"System.Object\".", StringComparison.OrdinalIgnoreCase))
					throw;
			}
			finally
			{
				AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, dataDirectory);
			}
		}

		[TestMethod]
		public void GetDataDirectory_IfTheValidateParameterIsFalseAndIfTheDataDirectoryIsNotSetAsString_ShouldReturnNull()
		{
			var dataDirectory = AppDomain.CurrentDomain.GetData(AppDomainExtension.DataDirectoryName);
			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, new object());
			var applicationDomain = (AppDomainWrapper) AppDomain.CurrentDomain;
			Assert.IsNull(applicationDomain.GetDataDirectory(false));
			AppDomain.CurrentDomain.SetData(AppDomainExtension.DataDirectoryName, dataDirectory);
		}

		#endregion
	}
}