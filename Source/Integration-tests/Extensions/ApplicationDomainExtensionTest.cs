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
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetDataDirectoryPath_IfTheDataDirectoryPathIsNotSet_ShouldThrowAnInvalidOperationException()
		{
			var applicationDomain = (AppDomainWrapper) AppDomain.CurrentDomain;

			try
			{
				applicationDomain.GetDataDirectoryPath();
			}

			catch(InvalidOperationException invalidOperationException)
			{
				if(string.Equals(invalidOperationException.Message, "The variable \"DataDirectory\" is not set for the application-domain.", StringComparison.OrdinalIgnoreCase))
					throw;
			}
		}

		#endregion
	}
}