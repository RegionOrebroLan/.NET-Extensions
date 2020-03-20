using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan.Security.Cryptography
{
	public class X509ChainStatusWrapper : Wrapper<X509ChainStatus>, IChainStatus
	{
		#region Constructors

		public X509ChainStatusWrapper(X509ChainStatus chainStatus) : base(chainStatus, nameof(chainStatus)) { }

		#endregion

		#region Properties

		public virtual string Information => this.WrappedInstance.StatusInformation;
		public virtual X509ChainStatusFlags Kind => this.WrappedInstance.Status;

		#endregion

		#region Methods

		public static X509ChainStatusWrapper FromX509ChainStatus(X509ChainStatus chainStatus)
		{
			return chainStatus;
		}

		#region Implicit operators

		public static implicit operator X509ChainStatusWrapper(X509ChainStatus chainStatus)
		{
			return new X509ChainStatusWrapper(chainStatus);
		}

		#endregion

		#endregion
	}
}