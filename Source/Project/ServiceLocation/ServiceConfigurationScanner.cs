using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RegionOrebroLan.ServiceLocation
{
	public class ServiceConfigurationScanner : IServiceConfigurationScanner
	{
		#region Methods

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual IEnumerable<IServiceConfigurationMapping> Scan(IEnumerable<Type> types)
		{
			if(types == null)
				throw new ArgumentNullException(nameof(types));

			var typeArray = types.ToArray();

			if(typeArray.Any(type => type == null))
				throw new ArgumentException("The type-collection can not contain null-values.", nameof(types));

			var mappings = new List<IServiceConfigurationMapping>();

			// ReSharper disable LoopCanBeConvertedToQuery
			foreach(var type in typeArray)
			{
				foreach(var configuration in type.GetCustomAttributes(typeof(IServiceConfiguration), true).Cast<IServiceConfiguration>())
				{
					mappings.Add(new ServiceConfigurationMapping
					{
						Configuration = configuration,
						Type = type
					});
				}
			}
			// ReSharper restore LoopCanBeConvertedToQuery

			return mappings.ToArray();
		}

		#endregion
	}
}