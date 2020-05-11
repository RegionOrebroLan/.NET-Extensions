using System;

namespace RegionOrebroLan.ServiceLocation
{
	[Obsolete("Start using Microsoft.Extensions.DependencyInjection.ServiceLifetime instead, https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions/. This will later be removed.")]
	public enum InstanceMode
	{
		New,
		Request,
		Singleton,
		Thread
	}
}