using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual ClaimsPrincipal Build()
		{
			try
			{
				var claimsIdentities = this.ClaimsIdentityBuilders.Where(claimsIdentityBuilder => claimsIdentityBuilder != null).Select(claimsIdentityBuilder => claimsIdentityBuilder.Build());

				return new ClaimsPrincipal(claimsIdentities);
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException("Could not build claims-principal.", exception);
			}
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public virtual IClaimsPrincipalBuilder Clone()
		{
			var clone = new ClaimsPrincipalBuilder();

			foreach(var claimsIdentityBuilder in this.ClaimsIdentityBuilders)
			{
				clone.ClaimsIdentityBuilders.Add(claimsIdentityBuilder.Clone());
			}

			return clone;
		}

		#endregion
	}
}