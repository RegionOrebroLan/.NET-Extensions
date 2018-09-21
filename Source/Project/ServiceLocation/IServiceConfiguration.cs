using System;

namespace RegionOrebroLan.ServiceLocation
{
	public interface IServiceConfiguration
	{
		#region Properties

		InstanceMode InstanceMode { get; }
		Type ServiceType { get; }

		#endregion
	}
}