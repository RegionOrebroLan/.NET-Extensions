using System;
using System.Collections.Generic;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface ICertificate
	{
		#region Properties

		bool Archived { get; }
		string FriendlyName { get; }
		bool HasPrivateKey { get; }
		string Issuer { get; }
		IDistinguishedName IssuerName { get; }
		DateTime NotAfter { get; }
		DateTime NotBefore { get; }
		IEnumerable<byte> RawData { get; }
		string SerialNumber { get; }
		IOid SignatureAlgorithm { get; }
		string Subject { get; }
		IDistinguishedName SubjectName { get; }
		string Thumbprint { get; }
		int Version { get; }

		#endregion
	}
}