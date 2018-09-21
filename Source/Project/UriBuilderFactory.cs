using System;
using RegionOrebroLan.Abstractions;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan
{
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(IUriBuilderFactory))]
	public class UriBuilderFactory : IUriBuilderFactory
	{
		#region Methods

		public virtual IUriBuilder Create()
		{
			return this.Create(new UriBuilder());
		}

		public virtual IUriBuilder Create(string uniformResourceIdentifier)
		{
			if(uniformResourceIdentifier == null)
				throw new ArgumentNullException(nameof(uniformResourceIdentifier));

			return this.Create(new Uri(uniformResourceIdentifier, UriKind.RelativeOrAbsolute));
		}

		public virtual IUriBuilder Create(IUri uri)
		{
			if(uri == null)
				throw new ArgumentNullException(nameof(uri));

			var concreteUri = (uri as IWrapper)?.WrappedInstance as Uri;

			return concreteUri == null ? this.Create(uri.OriginalString) : this.Create(concreteUri);
		}

		public virtual IUriBuilder Create(Uri uri)
		{
			if(uri == null)
				throw new ArgumentNullException(nameof(uri));

			return this.Create(new UriBuilder(uri));
		}

		protected internal virtual IUriBuilder Create(UriBuilder uriBuilder)
		{
			return new UriBuilderWrapper(uriBuilder);
		}

		#endregion
	}
}