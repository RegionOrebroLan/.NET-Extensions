using System;
using RegionOrebroLan.DependencyInjection;
using RegionOrebroLan.Security.Cryptography.Configuration;
using ObsoleteInstanceMode = RegionOrebroLan.ServiceLocation.InstanceMode;
using ObsoleteServiceConfigurationAttribute = RegionOrebroLan.ServiceLocation.ServiceConfigurationAttribute;

namespace RegionOrebroLan.Security.Cryptography
{
#pragma warning disable CS0618 // Type or member is obsolete
	[ObsoleteServiceConfiguration(InstanceMode = ObsoleteInstanceMode.Singleton, ServiceType = typeof(ICertificateResolver))]
#pragma warning restore CS0618 // Type or member is obsolete
	[ServiceConfiguration(ServiceType = typeof(ICertificateResolver))]
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