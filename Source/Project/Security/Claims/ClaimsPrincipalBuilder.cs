using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public class ClaimsPrincipalBuilder : IClaimsPrincipalBuilder
	{
		#region Fields

		private readonly IList<IClaimsIdentityBuilder> _claimsIdentityBuilders = new List<IClaimsIdentityBuilder>();

		#endregion

		#region Constructors

		public ClaimsPrincipalBuilder() { }

		public ClaimsPrincipalBuilder(ClaimsPrincipal claimsPrincipal)
		{
			claimsPrincipal = claimsPrincipal?.Clone();

			if(claimsPrincipal?.Identities == null)
				return;

			foreach(var claimsIdentity in claimsPrincipal.Identities)
			{
				this._claimsIdentityBuilders.Add(new ClaimsIdentityBuilder(claimsIdentity));
			}
		}

		#endregion

		#region Properties

		public virtual IList<IClaimsIdentityBuilder> ClaimsIdentityBuilders => this._claimsIdentityBuilders;

		#endregion

		#region Methods

		public virtual ClaimsPrincipal Build()
		{
			var claimsIdentities = this.ClaimsIdentityBuilders.Where(claimsIdentityBuilder => claimsIdentityBuilder != null).Select(claimsIdentityBuilder => claimsIdentityBuilder.Build());

			return new ClaimsPrincipal(claimsIdentities);
		}

		#endregion
	}
}