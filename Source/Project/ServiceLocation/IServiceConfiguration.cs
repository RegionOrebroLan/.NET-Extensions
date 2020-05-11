using System;

namespace RegionOrebroLan.ServiceLocation
{
	[Obsolete("Start using RegionOrebroLan.DependencyInjection.IServiceConfiguration instead, https://www.nuget.org/packages/RegionOrebroLan.DependencyInjection/. This will later be removed.")]
	public interface IServiceConfiguration
	{
		#region Properties

		InstanceMode InstanceMode { get; }
		Type ServiceType { get; }

		#endregion
	}
}