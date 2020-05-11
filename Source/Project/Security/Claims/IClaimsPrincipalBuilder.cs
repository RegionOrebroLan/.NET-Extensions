using System.Collections.Generic;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public interface IClaimsPrincipalBuilder
	{
		#region Properties

		IList<IClaimsIdentityBuilder> ClaimsIdentityBuilders { get; }

		#endregion

		#region Methods

		ClaimsPrincipal Build();

		#endregion
	}
}