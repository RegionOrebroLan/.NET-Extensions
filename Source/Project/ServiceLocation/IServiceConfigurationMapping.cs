using System;

namespace RegionOrebroLan.ServiceLocation
{
	public interface IServiceConfigurationMapping
	{
		#region Properties

		IServiceConfiguration Configuration { get; }
		Type Type { get; }

		#endregion
	}
}