using System;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan
{
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(IUriBuilderFactory))]
	public class UriBuilderFactory : IUriBuilderFactory
	{
		#region Methods

		public virtual IUriBuilder Create()
		{
			return new UriBuilderWrapper();
		}

		public virtual IUriBuilder Create(string uniformResourceIdentifier)
		{
			if(uniformResourceIdentifier == null)
				throw new ArgumentNullException(nameof(uniformResourceIdentifier));

			return new UriBuilderWrapper(uniformResourceIdentifier);
		}

		public virtual IUriBuilder Create(IUri uri)
		{
			if(uri == null)
				throw new ArgumentNullException(nameof(uri));

			return new UriBuilderWrapper(uri);
		}

		#endregion
	}
}