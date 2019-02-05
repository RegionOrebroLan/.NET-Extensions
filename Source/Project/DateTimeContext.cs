using System;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan
{
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(IDateTimeContext))]
	public class DateTimeContext : IDateTimeContext
	{
		#region Properties

		public virtual DateTime Now => DateTime.Now;
		public virtual DateTime UtcNow => DateTime.UtcNow;

		#endregion
	}
}