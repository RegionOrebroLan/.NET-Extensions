using System;

namespace RegionOrebroLan
{
	public interface IUriBuilderFactory
	{
		#region Methods

		IUriBuilder Create();
		IUriBuilder Create(string uniformResourceIdentifier);
		IUriBuilder Create(IUri uri);
		IUriBuilder Create(Uri uri);

		#endregion
	}
}