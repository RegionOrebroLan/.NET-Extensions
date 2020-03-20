using System;
using RegionOrebroLan.Security.Cryptography.Configuration;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan.Security.Cryptography
{
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(ICertificateResolver))]
	public class CertificateResolver : ICertificateResolver
	{
		#region Constructors

		public CertificateResolver(FileCertificateResolver fileCertificateResolver, StoreCertificateResolver storeCertificateResolver)
		{
			this.FileCertificateResolver = fileCertificateResolver ?? throw new ArgumentNullException(nameof(fileCertificateResolver));
			this.StoreCertificateResolver = storeCertificateResolver ?? throw new ArgumentNullException(nameof(storeCertificateResolver));
		}

		#endregion

		#region Properties

		protected internal virtual FileCertificateResolver FileCertificateResolver { get; }
		protected internal virtual StoreCertificateResolver StoreCertificateResolver { get; }

		#endregion

		#region Methods

		public virtual ICertificate Resolve(FileResolverOptions options)
		{
			if(options == null)
				throw new ArgumentNullException(nameof(options));

			return this.FileCertificateResolver.Resolve(options.Password, options.Path);
		}

		public virtual ICertificate Resolve(StoreResolverOptions options)
		{
			if(options == null)
				throw new ArgumentNullException(nameof(options));

			return this.StoreCertificateResolver.Resolve(options.Path, options.ValidOnly);
		}

		#endregion
	}
}