using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan.IntegrationTests.Extensions
{
	[TestClass]
	public class UriBuilderFactoryExtensionTest
	{
		#region Methods

		[TestMethod]
		public void Create_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldNotThrowAnInvalidOperationException()
		{
			new UriBuilderFactory().Create(new Uri("/", UriKind.Relative));
		}

		[TestMethod]
		public void Create_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldReturnAnUriBuilderWhereHostIsNull()
		{
			Assert.IsNull(new UriBuilderFactory().Create(new Uri("/", UriKind.Relative)).Host);
		}

		[TestMethod]
		public void Create_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldReturnAnUriBuilderWherePortIsNull()
		{
			Assert.IsNull(new UriBuilderFactory().Create(new Uri("/", UriKind.Relative)).Port);
		}

		[TestMethod]
		public void Create_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldReturnAnUriBuilderWhereSchemeIsNull()
		{
			Assert.IsNull(new UriBuilderFactory().Create(new Uri("/", UriKind.Relative)).Scheme);
		}

		[TestMethod]
		public void Create_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldReturnAnUriBuilderWhereUriIsAbsoluteReturnsFalse()
		{
			Assert.IsFalse(new UriBuilderFactory().Create(new Uri("/", UriKind.Relative)).Uri.IsAbsolute);
		}

		[TestMethod]
		public void Create_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldReturnAUriBuilderWithARelativeUri()
		{
			var uriBuilderFactory = new UriBuilderFactory();

			foreach(var item in new[] {"/", @"\", "Default.html", "/Default.html", "#Fragment", "/#Fragment", "Default.html#Fragment", "/Default.html#Fragment", "Directory/Default.html", "/Directory/Default.html", "Directory/Default.html#Fragment", "/Directory/Default.html#Fragment"})
			{
				Assert.IsFalse(uriBuilderFactory.Create(new Uri(item, UriKind.Relative)).Uri.IsAbsolute);
			}
		}

		#endregion
	}
}