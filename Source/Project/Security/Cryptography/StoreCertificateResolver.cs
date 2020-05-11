using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.DependencyInjection;
using ObsoleteInstanceMode = RegionOrebroLan.ServiceLocation.InstanceMode;
using ObsoleteServiceConfigurationAttribute = RegionOrebroLan.ServiceLocation.ServiceConfigurationAttribute;

namespace RegionOrebroLan.Security.Cryptography
{
#pragma warning disable CS0618 // Type or member is obsolete
	[ObsoleteServiceConfiguration(InstanceMode = ObsoleteInstanceMode.Singleton)]
#pragma warning restore CS0618 // Type or member is obsolete
	[ServiceConfiguration]
	public class StoreCertificateResolver
	{
		#region Fields

		private static readonly IEnumerable<X509FindType> _findTypes = new[]
		{
			X509FindType.FindByThumbprint,
			X509FindType.FindBySerialNumber,
			X509FindType.FindBySubjectDistinguishedName,
			X509FindType.FindBySubjectName,
			X509FindType.FindByIssuerDistinguishedName,
			X509FindType.FindByIssuerName
		};

		#endregion

		#region Properties

		protected internal virtual IEnumerable<X509FindType> FindTypes => _findTypes;

		#endregion

		#region Methods

		[SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code")]
		protected internal virtual X509Certificate2 GetInternal(StoreLocation storeLocation, StoreName storeName, object value, bool validOnly)
		{
			using(var store = new X509Store(storeName, storeLocation))
			{
				store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

				foreach(var findType in this.FindTypes)
				{
					var certificate = store.Certificates.Find(findType, value, validOnly).Cast<X509Certificate2>().FirstOrDefault();

					if(certificate != null)
						return certificate;
				}

				throw new InvalidOperationException($"Could not get a{(validOnly ? " valid" : string.Empty)} certificate with value \"{value}\" at store-location \"{storeLocation}\" and store-name \"{storeName}\".");
			}
		}

		public virtual ICertificate Resolve(string path, bool validOnly)
		{
			return (X509Certificate2Wrapper) this.ResolveInternal(path, validOnly);
		}

		protected internal virtual X509Certificate2 ResolveInternal(string path, bool validOnly)
		{
			if(path == null)
				throw new ArgumentNullException(nameof(path));

			try
			{
				var parts = path.Split(new[] {Path.DirectorySeparatorChar}, 4);

				var invalidPathException = new InvalidOperationException($"The path \"{path}\" is invalid.");

				if(parts.Length != 4)
					throw invalidPathException;

				if(!parts[0].Equals("CERT:", StringComparison.OrdinalIgnoreCase))
					throw invalidPathException;

				if(!Enum.TryParse<StoreLocation>(parts[1], true, out var storeLocation))
					throw invalidPathException;

				if(!Enum.TryParse<StoreName>(parts[2], true, out var storeName))
					throw invalidPathException;

				return this.GetInternal(storeLocation, storeName, parts[3], validOnly);
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException($"Could not resolve a{(validOnly ? " valid" : string.Empty)} certificate with the following path \"{path}\".", exception);
			}
		}

		#endregion
	}
}