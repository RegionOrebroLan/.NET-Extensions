using System;
using System.Collections.Generic;

namespace RegionOrebroLan.ServiceLocation
{
	[Obsolete("Start using RegionOrebroLan.DependencyInjection.IServiceConfigurationScanner instead, https://www.nuget.org/packages/RegionOrebroLan.DependencyInjection/. This will later be removed.")]
	public interface IServiceConfigurationScanner
	{
		#region Methods

		IEnumerable<IServiceConfigurationMapping> Scan(IEnumerable<Type> types);

		#endregion
	}
}