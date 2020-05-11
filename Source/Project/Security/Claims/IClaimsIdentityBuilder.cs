using System.Collections.Generic;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public interface IClaimsIdentityBuilder
	{
		#region Properties

		string AuthenticationType { get; set; }
		IList<IClaimBuilder> ClaimBuilders { get; }
		string Label { get; set; }
		string NameClaimType { get; set; }
		string RoleClaimType { get; set; }

		#endregion

		#region Methods

		ClaimsIdentity Build();

		#endregion
	}
}