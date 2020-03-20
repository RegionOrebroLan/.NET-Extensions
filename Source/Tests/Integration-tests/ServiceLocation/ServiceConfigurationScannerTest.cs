using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan.IntegrationTests.ServiceLocation
{
	[TestClass]
	public class ServiceConfigurationScannerTest
	{
		#region Fields

		public const int ExpectedNumberOfMappingsInTheAssembly = 3;

		#endregion

		#region Methods

		[TestMethod]
		public void Scan_ShouldWorkProperly()
		{
			var mappings = new ServiceConfigurationScanner().Scan(typeof(AppDomainWrapper).Assembly.GetTypes());

			Assert.AreEqual(ExpectedNumberOfMappingsInTheAssembly, mappings.Count());
		}

		#endregion
	}
}