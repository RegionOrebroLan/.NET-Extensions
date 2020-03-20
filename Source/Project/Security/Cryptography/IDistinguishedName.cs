using System.Collections.Generic;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface IDistinguishedName
	{
		#region Properties

		string Name { get; }
		IOid Oid { get; }
		IEnumerable<byte> RawData { get; }

		#endregion
	}
}