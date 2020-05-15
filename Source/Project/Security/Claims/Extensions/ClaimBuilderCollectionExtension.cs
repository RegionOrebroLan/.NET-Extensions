using System;

namespace RegionOrebroLan.Security.Claims.Extensions
{
	public static class ClaimBuilderCollectionExtension
	{
		#region Methods

		public static void Add(this IClaimBuilderCollection claimBuilderCollection, string type, string value)
		{
			claimBuilderCollection.Add(type, value, null);
		}

		public static void Add(this IClaimBuilderCollection claimBuilderCollection, string type, string value, string valueType)
		{
			claimBuilderCollection.Add(null, type, value, valueType);
		}

		public static void Add(this IClaimBuilderCollection claimBuilderCollection, string issuer, string type, string value, string valueType)
		{
			claimBuilderCollection.Add(issuer, null, type, value, valueType);
		}

		public static void Add(this IClaimBuilderCollection claimBuilderCollection, string issuer, string originalIssuer, string type, string value, string valueType)
		{
			if(claimBuilderCollection == null)
				throw new ArgumentNullException(nameof(claimBuilderCollection));

			var claimBuilder = claimBuilderCollection.CreateItem();

			claimBuilder.Issuer = issuer;
			claimBuilder.OriginalIssuer = originalIssuer;
			claimBuilder.Type = type;
			claimBuilder.Value = value;
			claimBuilder.ValueType = valueType;

			claimBuilderCollection.Add(claimBuilder);
		}

		#endregion
	}
}