using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.ServiceLocation;
using RegionOrebroLan.ServiceLocation.Extensions;

namespace RegionOrebroLan.IntegrationTests.ServiceLocation.Extensions
{
	[TestClass]
	public class ServiceConfigurationScannerExtensionTest
	{
		#region Fields

		private static readonly IServiceConfigurationScanner _serviceConfigurationScanner = new ServiceConfigurationScanner();

		#endregion

		#region Properties

		protected internal virtual IServiceConfigurationScanner ServiceConfigurationScanner => _serviceConfigurationScanner;

		#endregion

		#region Methods

		[TestMethod]
		public void Scan_ByAssemblies_ShouldWorkProperly()
		{
			const int expectedCount = ServiceConfigurationScannerTest.ExpectedNumberOfMappingsInTheAssembly + ServiceConfigurationScannerTest.ExpectedNumberOfMappingsInTheAssembly;
			var firstAssembly = typeof(DateTimeContext).Assembly;
			var secondAssembly = typeof(string).Assembly;
			IEnumerable<Assembly> assemblies = new[] {firstAssembly, secondAssembly, firstAssembly};
			Assert.AreEqual(expectedCount, this.ServiceConfigurationScanner.Scan(assemblies).Count());
		}

		[TestMethod]
		public void Scan_ByAssembly_ShouldWorkProperly()
		{
			Assert.AreEqual(ServiceConfigurationScannerTest.ExpectedNumberOfMappingsInTheAssembly, this.ServiceConfigurationScanner.Scan(typeof(DateTimeContext).Assembly).Count());
			Assert.AreEqual(0, this.ServiceConfigurationScanner.Scan(typeof(string).Assembly).Count());
		}

		[TestMethod]
		public void Scan_ByAssemblyParams_ShouldWorkProperly()
		{
			const int expectedCount = ServiceConfigurationScannerTest.ExpectedNumberOfMappingsInTheAssembly + ServiceConfigurationScannerTest.ExpectedNumberOfMappingsInTheAssembly;
			var firstAssembly = typeof(DateTimeContext).Assembly;
			var secondAssembly = typeof(string).Assembly;
			Assert.AreEqual(expectedCount, this.ServiceConfigurationScanner.Scan(firstAssembly, secondAssembly, firstAssembly).Count());
		}

		[TestMethod]
		public void Scan_ByType_ShouldWorkProperly()
		{
			Assert.AreEqual(1, this.ServiceConfigurationScanner.Scan(typeof(DateTimeContext)).Count());
			Assert.AreEqual(0, this.ServiceConfigurationScanner.Scan(typeof(string)).Count());
		}

		[TestMethod]
		public void Scan_ByTypeParams_ShouldWorkProperly()
		{
			var type = typeof(DateTimeContext);
			Assert.AreEqual(3, this.ServiceConfigurationScanner.Scan(type, type, type).Count());

			type = typeof(string);
			Assert.AreEqual(0, this.ServiceConfigurationScanner.Scan(type, type, type).Count());
		}

		#endregion
	}
}