using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RegionOrebroLan.Extensions
{
	public static class ApplicationDomainExtension
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DataDirectory")]
		public static string GetDataDirectoryPath(this IApplicationDomain applicationDomain)
		{
			if(applicationDomain == null)
				throw new ArgumentNullException(nameof(applicationDomain));

			const string dataDirectory = "DataDirectory";
			var dataDirectoryPath = (string) applicationDomain.GetData(dataDirectory);

			if(dataDirectoryPath == null)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The variable \"{0}\" is not set for the application-domain.", dataDirectory));

			return dataDirectoryPath;
		}

		#endregion
	}
}