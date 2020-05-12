using System;
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
		[ExpectedException(typeof(InvalidOperationException))]
		public void Build_IfTheTypePropertyIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new ClaimBuilder {Value = "value"}.Build();
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(invalidOperationException.InnerException is ArgumentNullException argumentNullException && string.Equals(argumentNullException.ParamName, "type", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Build_IfTheValuePropertyIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new ClaimBuilder {Type = "type"}.Build();
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(invalidOperationException.InnerException is ArgumentNullException argumentNullException && string.Equals(argumentNullException.ParamName, "value", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void Test()
		{
			var claimBuilder = new ClaimBuilder {Type = "type", Value = "value"};
			claimBuilder.Properties.Add("First", "First value");
			claimBuilder.Properties.Add("Second", "Second value");
			claimBuilder.Properties.Add("Third", "Third value");

			var claim = claimBuilder.Build();

			Assert.IsNotNull(claim);
			Assert.AreEqual(ClaimsIdentity.DefaultIssuer, claim.Issuer);
			Assert.AreEqual(ClaimsIdentity.DefaultIssuer, claim.OriginalIssuer);
			Assert.AreEqual("type", claim.Type);
			Assert.AreEqual("value", claim.Value);
			Assert.AreEqual(ClaimValueTypes.String, claim.ValueType);
			Assert.AreEqual(3, claim.Properties.Count);
		}

		#endregion
	}
}