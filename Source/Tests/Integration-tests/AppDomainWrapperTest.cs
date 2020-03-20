using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.IntegrationTests
{
	[TestClass]
	public class AppDomainWrapperTest
	{
		#region Methods

		[TestMethod]
		public void BaseDirectory_ShouldReturnTheBaseDirectoryFromTheWrappedAppDomain()
		{
			var appDomain = AppDomain.CurrentDomain;
			Assert.AreEqual(appDomain.BaseDirectory, new AppDomainWrapper(appDomain).BaseDirectory);
		}

		[TestMethod]
		public void GetData_ShouldCallGetDataOfTheWrappedAppDomain()
		{
			this.SetAndGetDataTest(Guid.NewGuid().ToString(), new object());
		}

		protected internal virtual void SetAndGetDataTest(string name, object data)
		{
			var appDomain = AppDomain.CurrentDomain;
			var appDomainWrapper = (AppDomainWrapper) appDomain;
			Assert.IsNull(appDomain.GetData(name));
			Assert.IsNull(appDomainWrapper.GetData(name));
			appDomainWrapper.SetData(name, data);
			Assert.IsTrue(ReferenceEquals(data, appDomain.GetData(name)));
			Assert.IsTrue(ReferenceEquals(data, appDomainWrapper.GetData(name)));
		}

		[TestMethod]
		public void SetData_WithTwoParameters_ShouldCallSetDataOfTheWrappedAppDomain()
		{
			this.SetAndGetDataTest(Guid.NewGuid().ToString(), new object());
		}

		#endregion
	}
}