using System;
using Microsoft.Extensions.Configuration;

namespace RegionOrebroLan.Configuration
{
	public class DynamicOptions
	{
		#region Properties

		[CLSCompliant(false)]
		public virtual IConfigurationSection Options { get; set; }

		public virtual string Type { get; set; }

		#endregion
	}
}