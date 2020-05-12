using System.Collections.Generic;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public interface IClaimsIdentityBuilder
	{
		#region Properties

		IClaimsIdentityBuilder ActorBuilder { get; set; }
		string AuthenticationType { get; set; }
		object BootstrapContext { get; set; }
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