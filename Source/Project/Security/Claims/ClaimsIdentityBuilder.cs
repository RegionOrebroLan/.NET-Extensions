using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public class ClaimsIdentityBuilder : IClaimsIdentityBuilder
	{
		#region Fields

		private IClaimsIdentityBuilder _actorBuilder;
		private string _authenticationType;
		private object _bootstrapContext;
		private readonly IClaimBuilderCollection _claimBuilders = new ClaimBuilderCollection();
		private string _label;
		private string _nameClaimType;
		private string _roleClaimType;

		#endregion

		#region Constructors

		public ClaimsIdentityBuilder() { }

		public ClaimsIdentityBuilder(ClaimsIdentity claimsIdentity)
		{
			claimsIdentity = claimsIdentity?.Clone();

			if(claimsIdentity?.Actor != null)
				this._actorBuilder = new ClaimsIdentityBuilder(claimsIdentity.Actor);

			this._authenticationType = claimsIdentity?.AuthenticationType;
			this._bootstrapContext = claimsIdentity?.BootstrapContext;
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

		public virtual IClaimsIdentityBuilder ActorBuilder
		{
			get => this._actorBuilder;
			set => this._actorBuilder = value;
		}

		public virtual string AuthenticationType
		{
			get => this._authenticationType;
			set => this._authenticationType = value;
		}

		public virtual object BootstrapContext
		{
			get => this._bootstrapContext;
			set => this._bootstrapContext = value;
		}

		public virtual IClaimBuilderCollection ClaimBuilders => this._claimBuilders;

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

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual ClaimsIdentity Build()
		{
			try
			{
				var claimsIdentity = new ClaimsIdentity(this.ClaimBuilders.Build(), this.AuthenticationType, this.NameClaimType, this.RoleClaimType)
				{
					Actor = this.ActorBuilder?.Build(),
					BootstrapContext = this.BootstrapContext,
					Label = this.Label
				};

				return claimsIdentity;
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException("Could not build claims-identity.", exception);
			}
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public virtual IClaimsIdentityBuilder Clone()
		{
			var clone = new ClaimsIdentityBuilder
			{
				BootstrapContext = this.BootstrapContext,
				ActorBuilder = this.ActorBuilder.Clone(),
				AuthenticationType = this.AuthenticationType,
				Label = this.Label,
				NameClaimType = this.NameClaimType,
				RoleClaimType = this.RoleClaimType
			};

			foreach(var claimBuilder in this.ClaimBuilders)
			{
				clone.ClaimBuilders.Add(claimBuilder.Clone());
			}

			return clone;
		}

		#endregion
	}
}