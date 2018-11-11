using System;

namespace RegionOrebroLan.Extensions
{
	public static class UriBuilderFactoryExtension
	{
		#region Methods

		public static IUriBuilder Create(this IUriBuilderFactory uriBuilderFactory, Uri uri)
		{
			if(uriBuilderFactory == null)
				throw new ArgumentNullException(nameof(uriBuilderFactory));

			return uriBuilderFactory.Create((UriWrapper) uri);
		}

		#endregion
	}
}