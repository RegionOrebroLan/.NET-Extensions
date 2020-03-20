using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan.UnitTests.Extensions
{
	[TestClass]
	public class StringExtensionTest
	{
		#region Methods

		[TestMethod]
		public void Like_Test()
		{
			Assert.IsTrue("Test".Like("test"));
			Assert.IsFalse("Test".Like("test", false));

			Assert.IsFalse("My Test Text".Like("test"));
			Assert.IsTrue("My Test Text".Like("*test*"));
			Assert.IsFalse("My Test Text".Like("*test*", false));
		}

		#endregion
	}
}