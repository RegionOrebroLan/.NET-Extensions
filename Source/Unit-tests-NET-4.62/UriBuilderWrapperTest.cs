using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests.Net462
{
	[TestClass]
	public class UriBuilderWrapperTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithStringParameter_IfTheUniformResourceIdentifierContainsQueryAndFragment_ShouldCreateAnUriBuilderWrapperThatReturnsAnUriWithoutDoubleQuestionMarksAndWithoutDoubleHashSigns()
		{
			const string uniformResourceIdentifier = "http://localhost/?Key=Value#Test";
			Assert.AreEqual(uniformResourceIdentifier, new UriBuilderWrapper(uniformResourceIdentifier).Uri.ToString());
		}

		[TestMethod]
		public void IsDotNetFrameworkContext_ShouldReturnTrue()
		{
			Assert.IsTrue(new UriBuilderWrapper().IsDotNetFrameworkContext);
		}

		[TestMethod]
		public void Uri_Get_IfTheFragmentHasBeenSetWithALeadingHashSign_ShouldReturnAnUriWithoutDoubleLeadingHashSigns()
		{
			var uriBuilderWrapper = new UriBuilderWrapper
			{
				Fragment = "#Test"
			};

			Assert.AreEqual("/#Test", uriBuilderWrapper.Uri.ToString());
		}

		[TestMethod]
		public void Uri_Get_IfTheQueryHasBeenSetWithALeadingQuestionMark_ShouldReturnAnUriWithoutDoubleLeadingQuestionMarks()
		{
			var uriBuilderWrapper = new UriBuilderWrapper
			{
				Query = "?Key=Value"
			};

			Assert.AreEqual("/?Key=Value", uriBuilderWrapper.Uri.ToString());
		}

		#endregion
	}
}