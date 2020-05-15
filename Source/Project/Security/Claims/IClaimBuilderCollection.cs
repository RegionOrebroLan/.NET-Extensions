using System.Collections.Generic;
using System.Security.Claims;

namespace RegionOrebroLan.Security.Claims
{
	public interface IClaimBuilderCollection : ICloneable<IClaimBuilderCollection>, IList<IClaimBuilder>
	{
		#region Properties

		string DefaultIssuer { get; set; }
		string DefaultOriginalIssuer { get; set; }
		string DefaultValueType { get; set; }

		#endregion

		#region Methods

		IList<Claim> Build();
		IClaimBuilder CreateItem();

		#endregion
	}
}