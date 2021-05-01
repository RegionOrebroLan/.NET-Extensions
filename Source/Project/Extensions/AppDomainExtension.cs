using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace RegionOrebroLan.Extensions
{
	public static class AppDomainExtension
	{
		#region Fields

		public const string DataDirectoryName = "DataDirectory";

		#endregion

		#region Methods

		public static string GetDataDirectory(this AppDomain appDomain, bool validate = true)
		{
			if(appDomain == null)
				throw new ArgumentNullException(nameof(appDomain));

			var dataDirectory = appDomain.GetData(DataDirectoryName);

			if(dataDirectory == null)
				return null;

			var dataDirectoryType = dataDirectory.GetType();
			var validDataDirectoryType = typeof(string);

			if(dataDirectoryType == validDataDirectoryType)
				return (string) dataDirectory;

			if(validate)
				throw new InvalidOperationException($"The current value for \"{DataDirectoryName}\" is invalid. The value should be of type \"{validDataDirectoryType}\" but is of type \"{dataDirectoryType}\".");

			return null;
		}

		public static bool RemoveDataDirectory(this AppDomain appDomain)
		{
			if(appDomain == null)
				throw new ArgumentNullException(nameof(appDomain));

			if(appDomain.GetData(DataDirectoryName) == null)
				return false;

			appDomain.SetData(DataDirectoryName, null);

			return true;
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public static void SetDataDirectory(this AppDomain appDomain, string path, bool validate = false)
		{
			if(appDomain == null)
				throw new ArgumentNullException(nameof(appDomain));

			if(validate)
			{
				if(path == null)
					throw new ArgumentNullException(nameof(path));

				if(path.Length == 0)
					throw new ArgumentException("The path can not be empty.", nameof(path));

				if(!Directory.Exists(path))
					throw new ArgumentException("The path is invalid.", nameof(path), new DirectoryNotFoundException($"The directory \"{path}\" does not exist."));
			}

			appDomain.SetData(DataDirectoryName, path);
		}

		#endregion
	}
}