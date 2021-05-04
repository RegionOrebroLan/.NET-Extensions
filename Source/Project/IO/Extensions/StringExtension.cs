using System;

namespace RegionOrebroLan.IO.Extensions
{
	public static class StringExtension
	{
		#region Fields

		private static readonly IPathSubstitutionResolver _pathSubstitutionResolver = new PathSubstitutionResolver(new AppDomainWrapper(AppDomain.CurrentDomain));

		#endregion

		#region Methods

		public static string ResolveDataDirectorySubstitution(this string path)
		{
			try
			{
				return _pathSubstitutionResolver.ResolveDataDirectoryAsync(path).Result;
			}
			catch(AggregateException aggregateException)
			{
				throw aggregateException.InnerException ?? aggregateException;
			}
		}

		#endregion
	}
}