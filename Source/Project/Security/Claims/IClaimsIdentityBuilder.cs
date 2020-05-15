using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public interface IClaimsIdentityBuilder : ICloneable<IClaimsIdentityBuilder>
	{
		#region Properties

		IClaimsIdentityBuilder ActorBuilder { get; set; }
		string AuthenticationType { get; set; }
		object BootstrapContext { get; set; }
		IClaimBuilderCollection ClaimBuilders { get; }
		string Label { get; set; }
		string NameClaimType { get; set; }
		string RoleClaimType { get; set; }

		#endregion

		#region Methods

		ClaimsIdentity Build();

		#endregion
	}
}