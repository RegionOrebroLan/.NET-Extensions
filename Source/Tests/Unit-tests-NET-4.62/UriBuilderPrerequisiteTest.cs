using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests.Net462
{
	[TestClass]
	public class UriBuilderPrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void Fragment_Set_IfTheValueStartsWithAHashSign_ShouldResultInAGetValueOfTwoLeadingHashSigns()
		{
			var uriBuilder = new UriBuilder
			{
				Fragment = "#"
			};

			Assert.AreEqual("##", uriBuilder.Fragment);

			uriBuilder = new UriBuilder
			{
				Fragment = "#Test"
			};

			Assert.AreEqual("##Test", uriBuilder.Fragment);
		}

		[TestMethod]
		public void Query_Set_IfTheValueStartsWithAQuestionMark_ShouldResultInAGetValueOfTwoLeadingQuestionMarks()
		{
			var uriBuilder = new UriBuilder
			{
				Query = "?"
			};

			Assert.AreEqual("??", uriBuilder.Query);

			uriBuilder = new UriBuilder
			{
				Query = "?Key=Value"
			};

			Assert.AreEqual("??Key=Value", uriBuilder.Query);
		}

		[TestMethod]
		public void Type_Assembly_Name_ShouldReturnSystem()
		{
			Assert.AreEqual("System", typeof(UriBuilder).Assembly.GetName().Name);
		}

		#endregion
	}
}