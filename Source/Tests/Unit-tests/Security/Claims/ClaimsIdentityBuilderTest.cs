using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Security.Claims;

namespace RegionOrebroLan.UnitTests.Security.Claims
{
	[TestClass]
	public class ClaimsIdentityBuilderTest
	{
		#region Methods

		[TestMethod]
		public void Test()
		{
			var claimsIdentityBuilder = new ClaimsIdentityBuilder
			{
				AuthenticationType = "Test",
				Label = "Test-label",
				NameClaimType = ClaimTypes.Name,
				RoleClaimType = ClaimTypes.Role
			};

			claimsIdentityBuilder.ClaimBuilders.Add(new ClaimBuilder
			{
				Type = "type", Value = "value"
			});

			var claimsIdentity = claimsIdentityBuilder.Build();

			Assert.IsNotNull(claimsIdentity);
			Assert.IsNull(claimsIdentity.Actor);
			Assert.AreEqual("Test", claimsIdentity.AuthenticationType);
			Assert.IsNull(claimsIdentity.BootstrapContext);
			Assert.AreEqual("Test-label", claimsIdentity.Label);
			Assert.AreEqual(ClaimTypes.Name, claimsIdentity.NameClaimType);
			Assert.AreEqual(ClaimTypes.Role, claimsIdentity.RoleClaimType);

			Assert.AreEqual(ClaimsIdentity.DefaultIssuer, claimsIdentity.Claims.First().Issuer);
			Assert.AreEqual(ClaimsIdentity.DefaultIssuer, claimsIdentity.Claims.First().OriginalIssuer);
			Assert.AreEqual("type", claimsIdentity.Claims.First().Type);
			Assert.AreEqual("value", claimsIdentity.Claims.First().Value);
			Assert.AreEqual(ClaimValueTypes.String, claimsIdentity.Claims.First().ValueType);
		}

		#endregion
	}
}