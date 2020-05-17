using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan.Security.Cryptography
{
	[Obsolete("Overkill. This will be removed.")]
	public class X509ChainElementWrapper : Wrapper<X509ChainElement>, IChainLink
	{
		#region Constructors

		public X509ChainElementWrapper(X509ChainElement chainElement) : base(chainElement, nameof(chainElement)) { }

		#endregion

		#region Properties

		public virtual ICertificate Certificate => (X509Certificate2Wrapper) this.WrappedInstance.Certificate;
		public virtual string Information => this.WrappedInstance.Information;
		public virtual IEnumerable<IChainStatus> Status => this.WrappedInstance.ChainElementStatus.Select(item => (X509ChainStatusWrapper) item);

		#endregion

		#region Methods

		public static X509ChainElementWrapper FromX509ChainElement(X509ChainElement chainElement)
		{
			return chainElement;
		}

		#region Implicit operators

		public static implicit operator X509ChainElementWrapper(X509ChainElement chainElement)
		{
			return chainElement != null ? new X509ChainElementWrapper(chainElement) : null;
		}

		#endregion

		#endregion
	}
}