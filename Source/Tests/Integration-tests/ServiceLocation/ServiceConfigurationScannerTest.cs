using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan.IntegrationTests.ServiceLocation
{
	[TestClass]
	public class ServiceConfigurationScannerTest
	{
		#region Fields

		public const int ExpectedNumberOfMappingsInTheAssembly = 6;

		#endregion

		#region Methods

		[TestMethod]
		public void Scan_ShouldWorkProperly()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			var mappings = new ServiceConfigurationScanner().Scan(typeof(AppDomainWrapper).Assembly.GetTypes());
#pragma warning restore CS0618 // Type or member is obsolete

			Assert.AreEqual(ExpectedNumberOfMappingsInTheAssembly, mappings.Count());
		}

		#endregion
	}
}