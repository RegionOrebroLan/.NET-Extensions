using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public class ClaimBuilder : IClaimBuilder
	{
		#region Fields

		private string _issuer;
		private string _originalIssuer;
		private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();
		private string _type;
		private string _value;
		private string _valueType;

		#endregion

		#region Constructors

		public ClaimBuilder() { }

		public ClaimBuilder(Claim claim)
		{
			claim = claim?.Clone();

			this._issuer = claim?.Issuer;
			this._originalIssuer = claim?.OriginalIssuer;
			this._type = claim?.Type;
			this._value = claim?.Value;
			this._valueType = claim?.ValueType;

			if(claim?.Properties == null)
				return;

			foreach(var property in claim.Properties)
			{
				this._properties.Add(property.Key, property.Value);
			}
		}

		#endregion

		#region Properties

		public virtual string Issuer
		{
			get => this._issuer;
			set => this._issuer = value;
		}

		public virtual string OriginalIssuer
		{
			get => this._originalIssuer;
			set => this._originalIssuer = value;
		}

		public virtual IDictionary<string, string> Properties => this._properties;

		public virtual string Type
		{
			get => this._type;
			set => this._type = value;
		}

		public virtual string Value
		{
			get => this._value;
			set => this._value = value;
		}

		public virtual string ValueType
		{
			get => this._valueType;
			set => this._valueType = value;
		}

		#endregion

		#region Methods

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual Claim Build()
		{
			try
			{
				var claim = new Claim(this.Type, this.Value, this.ValueType, this.Issuer, this.OriginalIssuer);

				foreach(var property in this.Properties)
				{
					claim.Properties.Add(property.Key, property.Value);
				}

				return claim;
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException("Could not build claim.", exception);
			}
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public virtual IClaimBuilder Clone()
		{
			var clone = new ClaimBuilder
			{
				Issuer = this.Issuer,
				OriginalIssuer = this.OriginalIssuer,
				Type = this.Type,
				Value = this.Value,
				ValueType = this.ValueType,
			};

			foreach(var property in this.Properties)
			{
				clone.Properties.Add(property.Key, property.Value);
			}

			return clone;
		}

		#endregion
	}
}