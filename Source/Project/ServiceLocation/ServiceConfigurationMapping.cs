using System;

namespace RegionOrebroLan.ServiceLocation
{
	[Obsolete("Start using RegionOrebroLan.DependencyInjection.ServiceConfigurationMapping instead, https://www.nuget.org/packages/RegionOrebroLan.DependencyInjection/. This will later be removed.")]
	public class ServiceConfigurationMapping : IServiceConfigurationMapping
	{
		#region Properties

		public virtual IServiceConfiguration Configuration { get; set; }
		public virtual Type Type { get; set; }

		#endregion
	}
}