using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan.Security.Cryptography
{
	public class X500DistinguishedNameWrapper : Wrapper<X500DistinguishedName>, IDistinguishedName
	{
		#region Constructors

		public X500DistinguishedNameWrapper(X500DistinguishedName distinguishedName) : base(distinguishedName, nameof(distinguishedName)) { }

		#endregion

		#region Properties

		public virtual string Name => this.WrappedInstance.Name;
		public virtual IOid Oid => (OidWrapper) this.WrappedInstance.Oid;
		public virtual IEnumerable<byte> RawData => this.WrappedInstance.RawData;

		#endregion

		#region Methods

		public static X500DistinguishedNameWrapper FromX500DistinguishedName(X500DistinguishedName distinguishedName)
		{
			return distinguishedName;
		}

		#region Implicit operators

		public static implicit operator X500DistinguishedNameWrapper(X500DistinguishedName distinguishedName)
		{
			return distinguishedName != null ? new X500DistinguishedNameWrapper(distinguishedName) : null;
		}

		#endregion

		#endregion
	}
}