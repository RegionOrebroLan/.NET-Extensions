using System;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan.Extensions
{
	public static class ApplicationDomainExtension
	{
		#region Methods

		public static string GetDataDirectory(this IApplicationDomain applicationDomain, bool validate = true)
		{
			if(applicationDomain == null)
				throw new ArgumentNullException(nameof(applicationDomain));

			if(applicationDomain is not IWrapper<AppDomain> appDomainWrapper)
				throw new InvalidOperationException($"This method only supports {nameof(IApplicationDomain)}-instances implementing {nameof(IWrapper<AppDomain>)}.");

			return appDomainWrapper.WrappedInstance.GetDataDirectory(validate);
		}

		[Obsolete("Use GetDataDirectory instead.")]
		public static string GetDataDirectoryPath(this IApplicationDomain applicationDomain)
		{
			return applicationDomain.GetDataDirectory();
		}

		#endregion
	}
}