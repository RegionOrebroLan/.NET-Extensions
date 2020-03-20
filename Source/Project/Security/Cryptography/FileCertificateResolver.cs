using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan.Security.Cryptography
{
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton)]
	public class FileCertificateResolver
	{
		#region Constructors

		public FileCertificateResolver(IApplicationDomain applicationDomain)
		{
			this.ApplicationDomain = applicationDomain ?? throw new ArgumentNullException(nameof(applicationDomain));
		}

		#endregion

		#region Properties

		protected internal virtual IApplicationDomain ApplicationDomain { get; }

		#endregion

		#region Methods

		public virtual ICertificate Resolve(string password, string path)
		{
			return (X509Certificate2Wrapper) this.ResolveInternal(password, path);
		}

		protected internal virtual X509Certificate2 ResolveInternal(string password, string path)
		{
			if(path == null)
				throw new ArgumentNullException(nameof(path));

			try
			{
				if(!Path.IsPathRooted(path))
					path = Path.Combine(this.ApplicationDomain.BaseDirectory, path);

				return new X509Certificate2(path, password);
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException($"Could not resolve the certificate with the following path \"{path}\".", exception);
			}
		}

		#endregion
	}
}