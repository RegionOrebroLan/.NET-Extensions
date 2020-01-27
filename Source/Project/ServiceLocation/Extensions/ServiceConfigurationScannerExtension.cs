using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace RegionOrebroLan.ServiceLocation.Extensions
{
	public static class ServiceConfigurationScannerExtension
	{
		#region Methods

		public static IEnumerable<IServiceConfigurationMapping> Scan(this IServiceConfigurationScanner serviceConfigurationScanner, Assembly assembly)
		{
			if(serviceConfigurationScanner == null)
				throw new ArgumentNullException(nameof(serviceConfigurationScanner));

			if(assembly == null)
				throw new ArgumentNullException(nameof(assembly));

			return serviceConfigurationScanner.Scan(new[] {assembly});
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public static IEnumerable<IServiceConfigurationMapping> Scan(this IServiceConfigurationScanner serviceConfigurationScanner, IEnumerable<Assembly> assemblies)
		{
			if(serviceConfigurationScanner == null)
				throw new ArgumentNullException(nameof(serviceConfigurationScanner));

			if(assemblies == null)
				throw new ArgumentNullException(nameof(assemblies));

			var assemblyArray = assemblies.ToArray();

			if(assemblyArray.Any(assembly => assembly == null))
				throw new ArgumentException("The assembly-collection can not contain null-values.", nameof(assemblies));

			return serviceConfigurationScanner.Scan(assemblyArray.SelectMany(assembly => assembly.GetTypes()));
		}

		public static IEnumerable<IServiceConfigurationMapping> Scan(this IServiceConfigurationScanner serviceConfigurationScanner, params Assembly[] assemblies)
		{
			if(serviceConfigurationScanner == null)
				throw new ArgumentNullException(nameof(serviceConfigurationScanner));

			return serviceConfigurationScanner.Scan((IEnumerable<Assembly>) assemblies);
		}

		public static IEnumerable<IServiceConfigurationMapping> Scan(this IServiceConfigurationScanner serviceConfigurationScanner, Type type)
		{
			if(serviceConfigurationScanner == null)
				throw new ArgumentNullException(nameof(serviceConfigurationScanner));

			if(type == null)
				throw new ArgumentNullException(nameof(type));

			return serviceConfigurationScanner.Scan(new[] {type});
		}

		public static IEnumerable<IServiceConfigurationMapping> Scan(this IServiceConfigurationScanner serviceConfigurationScanner, params Type[] types)
		{
			if(serviceConfigurationScanner == null)
				throw new ArgumentNullException(nameof(serviceConfigurationScanner));

			return serviceConfigurationScanner.Scan(types);
		}

		#endregion
	}
}