using System;
using RegionOrebroLan.DependencyInjection;
using ObsoleteInstanceMode = RegionOrebroLan.ServiceLocation.InstanceMode;
using ObsoleteServiceConfigurationAttribute = RegionOrebroLan.ServiceLocation.ServiceConfigurationAttribute;

namespace RegionOrebroLan
{
#pragma warning disable CS0618 // Type or member is obsolete
	[ObsoleteServiceConfiguration(InstanceMode = ObsoleteInstanceMode.Singleton, ServiceType = typeof(IUriBuilderFactory))]
#pragma warning restore CS0618 // Type or member is obsolete
	[ServiceConfiguration(ServiceType = typeof(IUriBuilderFactory))]
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