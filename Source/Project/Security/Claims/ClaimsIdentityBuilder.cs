using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public class ClaimsIdentityBuilder : IClaimsIdentityBuilder
	{
		#region Fields

		private string _authenticationType;
		private readonly IList<IClaimBuilder> _claimBuilders = new List<IClaimBuilder>();
		private string _label;
		private string _nameClaimType;
		private string _roleClaimType;

		#endregion

		#region Constructors

		public ClaimsIdentityBuilder() { }

		public ClaimsIdentityBuilder(ClaimsIdentity claimsIdentity)
		{
			claimsIdentity = claimsIdentity?.Clone();

			this._authenticationType = claimsIdentity?.AuthenticationType;
			this._label = claimsIdentity?.Label;
			this._nameClaimType = claimsIdentity?.NameClaimType;
			this._roleClaimType = claimsIdentity?.RoleClaimType;

			if(claimsIdentity?.Claims == null)
				return;

			foreach(var claim in claimsIdentity.Claims)
			{
				this._claimBuilders.Add(new ClaimBuilder(claim));
			}
		}

		#endregion

		#region Properties

		public virtual string AuthenticationType
		{
			get => this._authenticationType;
			set => this._authenticationType = value;
		}

		public virtual IList<IClaimBuilder> ClaimBuilders => this._claimBuilders;

		public virtual string Label
		{
			get => this._label;
			set => this._label = value;
		}

		public virtual string NameClaimType
		{
			get => this._nameClaimType;
			set => this._nameClaimType = value;
		}

		public virtual string RoleClaimType
		{
			get => this._roleClaimType;
			set => this._roleClaimType = value;
		}

		#endregion

		#region Methods

		public virtual ClaimsIdentity Build()
		{
			var claims = this.ClaimBuilders.Where(claimBuilder => claimBuilder != null).Select(claimBuilder => claimBuilder.Build());

			return new ClaimsIdentity(claims, this.AuthenticationType, this.NameClaimType, this.RoleClaimType)
			{
				Label = this.Label
			};
		}

		#endregion
	}
}