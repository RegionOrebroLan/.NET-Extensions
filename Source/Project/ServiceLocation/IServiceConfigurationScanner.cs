using System;
using System.Collections.Generic;

namespace RegionOrebroLan.ServiceLocation
{
	public interface IServiceConfigurationScanner
	{
		#region Methods

		IEnumerable<IServiceConfigurationMapping> Scan(IEnumerable<Type> types);

		#endregion
	}
}