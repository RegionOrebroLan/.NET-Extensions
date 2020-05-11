using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Security.Claims;

namespace RegionOrebroLan.UnitTests.Security.Claims
{
	[TestClass]
	public class ClaimBuilderTest
	{
		#region Methods

		[TestMethod]
		public void Test()
		{
			var claim = new ClaimBuilder {Type = "type", Value = "value"}.Build();

			Assert.IsNotNull(claim);
			Assert.AreEqual(ClaimsIdentity.DefaultIssuer, claim.Issuer);
			Assert.AreEqual(ClaimsIdentity.DefaultIssuer, claim.OriginalIssuer);
			Assert.AreEqual("type", claim.Type);
			Assert.AreEqual("value", claim.Value);
			Assert.AreEqual(ClaimValueTypes.String, claim.ValueType);
		}

		#endregion
	}
}