using System.Security.Cryptography;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan.Security.Cryptography
{
	public class OidWrapper : Wrapper<Oid>, IOid
	{
		#region Constructors

		public OidWrapper(Oid oid) : base(oid, nameof(oid)) { }

		#endregion

		#region Properties

		public virtual string FriendlyName
		{
			get => this.WrappedInstance.FriendlyName;
			set => this.WrappedInstance.FriendlyName = value;
		}

		public virtual string Value
		{
			get => this.WrappedInstance.Value;
			set => this.WrappedInstance.Value = value;
		}

		#endregion

		#region Methods

		public static OidWrapper FromOid(Oid oid)
		{
			return oid;
		}

		#region Implicit operators

		public static implicit operator OidWrapper(Oid oid)
		{
			return oid != null ? new OidWrapper(oid) : null;
		}

		#endregion

		#endregion
	}
}