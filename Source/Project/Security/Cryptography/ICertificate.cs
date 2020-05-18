using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography
{
	public interface ICertificate : IDisposable
	{
		#region Properties

		bool Archived { get; set; }
		string FriendlyName { get; set; }
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

		#region Methods

		string GetNameInformation(X509NameType nameType, bool forIssuer);
		void Reset();
		string ToString(bool verbose);

		#endregion
	}
}