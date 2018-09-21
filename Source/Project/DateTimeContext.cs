using System;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan
{
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(IDateTimeContext))]
	public class DateTimeContext : IDateTimeContext
	{
		#region Properties

		public virtual DateTime Now => DateTime.Now;

		#endregion
	}
}