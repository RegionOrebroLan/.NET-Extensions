using System;

namespace RegionOrebroLan.ServiceLocation
{
	[Obsolete("Start using RegionOrebroLan.DependencyInjection.IServiceConfigurationMapping instead, https://www.nuget.org/packages/RegionOrebroLan.DependencyInjection/. This will later be removed.")]
	public interface IServiceConfigurationMapping
	{
		#region Properties

		IServiceConfiguration Configuration { get; }
		Type Type { get; }

		#endregion
	}
}