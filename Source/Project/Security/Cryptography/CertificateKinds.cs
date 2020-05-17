using System;

namespace RegionOrebroLan.Security.Cryptography
{
	[Flags]
	public enum CertificateKinds
	{
		Chained = 1,
		SelfSigned = 2,
		All = Chained | SelfSigned
	}
}