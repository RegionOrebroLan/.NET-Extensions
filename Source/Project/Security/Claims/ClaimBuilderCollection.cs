using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public class ClaimBuilderCollection : List<IClaimBuilder>, IClaimBuilderCollection
	{
		#region Fields

		private Func<string, bool> _valueIsEmptyFunction;

		#endregion

		#region Properties

		public virtual string DefaultIssuer { get; set; }
		public virtual string DefaultOriginalIssuer { get; set; }
		public virtual string DefaultValueType { get; set; }

		public virtual Func<string, bool> ValueIsEmptyFunction
		{
			get => this._valueIsEmptyFunction ??= string.IsNullOrWhiteSpace;
			set => this._valueIsEmptyFunction = value;
		}

		#endregion

		#region Methods

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual IList<Claim> Build()
		{
			try
			{
				var claims = new List<Claim>();

				foreach(var claimBuilder in this.Where(claimBuilder => claimBuilder.Type != null && !this.ValueIsEmptyFunction(claimBuilder.Value)))
				{
					var clone = claimBuilder.Clone();

					if(string.IsNullOrEmpty(clone.Issuer) && !string.IsNullOrEmpty(this.DefaultIssuer))
						clone.Issuer = this.DefaultIssuer;

					if(string.IsNullOrEmpty(clone.OriginalIssuer) && !string.IsNullOrEmpty(this.DefaultOriginalIssuer))
						clone.OriginalIssuer = this.DefaultOriginalIssuer;

					if(string.IsNullOrEmpty(clone.ValueType) && !string.IsNullOrEmpty(this.DefaultValueType))
						clone.ValueType = this.DefaultValueType;

					claims.Add(clone.Build());
				}

				return claims.OrderBy(claim => claim.Type, StringComparer.OrdinalIgnoreCase).ToList();
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException("Could not build claim-collection.", exception);
			}
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public virtual IClaimBuilderCollection Clone()
		{
			var clone = new ClaimBuilderCollection
			{
				DefaultIssuer = this.DefaultIssuer,
				DefaultOriginalIssuer = this.DefaultOriginalIssuer,
				DefaultValueType = this.DefaultValueType,
				ValueIsEmptyFunction = this.ValueIsEmptyFunction
			};

			clone.AddRange(this.Select(claimBuilder => claimBuilder.Clone()));

			return clone;
		}

		public virtual IClaimBuilder CreateItem()
		{
			return new ClaimBuilder();
		}

		#endregion
	}
}