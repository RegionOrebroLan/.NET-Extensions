using System;
using System.Threading.Tasks;
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

		public virtual async Task<ICertificate> ResolveAsync(ResolverOptions options)
		{
			return options switch
			{
				null => throw new ArgumentNullException(nameof(options)),
				FileResolverOptions fileResolverOptions => await this.ResolveAsync(fileResolverOptions).ConfigureAwait(false),
				StoreResolverOptions storeResolverOptions => await this.ResolveAsync(storeResolverOptions).ConfigureAwait(false),
				_ => throw new NotImplementedException($"Resolving certificates with options of type \"{options.GetType()}\" is not implemented.")
			};
		}

		protected internal virtual async Task<ICertificate> ResolveAsync(FileResolverOptions options)
		{
			if(options == null)
				throw new ArgumentNullException(nameof(options));

			return await this.FileCertificateResolver.ResolveAsync(options.Password, options.Path).ConfigureAwait(false);
		}

		protected internal virtual async Task<ICertificate> ResolveAsync(StoreResolverOptions options)
		{
			if(options == null)
				throw new ArgumentNullException(nameof(options));

			return await this.StoreCertificateResolver.ResolveAsync(options.Path, options.ValidOnly).ConfigureAwait(false);
		}

		#endregion
	}
}