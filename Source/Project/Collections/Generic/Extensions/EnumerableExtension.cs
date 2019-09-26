using System.Collections.Generic;
using RegionOrebroLan.Collections.Extensions;

namespace RegionOrebroLan.Collections.Generic.Extensions
{
	public static class EnumerableExtension
	{
		#region Methods

		public static IEnumerable<IIndexed<T>> Indexed<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.IndexedInternal<Indexed<T>, T>((index, value) => new Indexed<T>(index, value), () => new List<Indexed<T>>());
		}

		#endregion
	}
}