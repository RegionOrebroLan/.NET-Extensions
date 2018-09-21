using System;

namespace RegionOrebroLan.ServiceLocation
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ServiceConfigurationAttribute : Attribute, IServiceConfiguration
	{
		#region Properties

		public InstanceMode InstanceMode { get; set; } = InstanceMode.New;
		public Type ServiceType { get; set; }

		#endregion
	}
}