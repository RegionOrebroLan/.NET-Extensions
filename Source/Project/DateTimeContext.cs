using System;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan
{
#pragma warning disable CS0618 // Type or member is obsolete
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(IDateTimeContext))]
#pragma warning restore CS0618 // Type or member is obsolete
	public class DateTimeContext : IDateTimeContext
	{
		#region Properties

		public virtual DateTime Now => DateTime.Now;
		public virtual DateTime UtcNow => DateTime.UtcNow;

		#endregion
	}
}