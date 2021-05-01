using System;
using System.Globalization;

namespace RegionOrebroLan.Extensions
{
	public static class ApplicationDomainExtension
	{
		#region Methods

		public static string GetDataDirectoryPath(this IApplicationDomain applicationDomain)
		{
			if(applicationDomain == null)
				throw new ArgumentNullException(nameof(applicationDomain));

			var dataDirectoryPath = (string) applicationDomain.GetData(AppDomainExtension.DataDirectoryName);

			if(dataDirectoryPath == null)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The variable \"{0}\" is not set for the application-domain.", AppDomainExtension.DataDirectoryName));

			return dataDirectoryPath;
		}

		#endregion
	}
}