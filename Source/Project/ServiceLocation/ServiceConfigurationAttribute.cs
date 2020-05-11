using System;

namespace RegionOrebroLan.ServiceLocation
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	[Obsolete("Start using RegionOrebroLan.DependencyInjection.ServiceConfigurationAttribute instead, https://www.nuget.org/packages/RegionOrebroLan.DependencyInjection/. This will later be removed.")]
	public sealed class ServiceConfigurationAttribute : Attribute, IServiceConfiguration
	{
		#region Properties

		public InstanceMode InstanceMode { get; set; } = InstanceMode.New;
		public Type ServiceType { get; set; }

		#endregion
	}
}