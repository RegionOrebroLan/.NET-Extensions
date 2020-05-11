using System;
using RegionOrebroLan.DependencyInjection;
using ObsoleteInstanceMode = RegionOrebroLan.ServiceLocation.InstanceMode;
using ObsoleteServiceConfigurationAttribute = RegionOrebroLan.ServiceLocation.ServiceConfigurationAttribute;

namespace RegionOrebroLan
{
#pragma warning disable CS0618 // Type or member is obsolete
	[ObsoleteServiceConfiguration(InstanceMode = ObsoleteInstanceMode.Singleton, ServiceType = typeof(IDateTimeContext))]
#pragma warning restore CS0618 // Type or member is obsolete
	[ServiceConfiguration(ServiceType = typeof(IDateTimeContext))]
	public class DateTimeContext : IDateTimeContext
	{
		#region Properties

		public virtual DateTime Now => DateTime.Now;
		public virtual DateTime UtcNow => DateTime.UtcNow;

		#endregion
	}
}