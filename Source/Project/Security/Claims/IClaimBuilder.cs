using System.Collections.Generic;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public interface IClaimBuilder
	{
		#region Properties

		string Issuer { get; set; }
		string OriginalIssuer { get; set; }
		IDictionary<string, string> Properties { get; }
		string Type { get; set; }
		string Value { get; set; }
		string ValueType { get; set; }

		#endregion

		#region Methods

		Claim Build();

		#endregion
	}
}