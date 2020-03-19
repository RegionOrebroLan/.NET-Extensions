using System;
using System.Collections.Generic;

namespace RegionOrebroLan.Collections.Generic.Extensions
{
	public static class CollectionExtension
	{
		#region Methods

		public static void Add<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			if(collection == null)
				throw new ArgumentNullException(nameof(collection));

			if(items == null)
				throw new ArgumentNullException(nameof(items));

			foreach(var item in items)
			{
				collection.Add(item);
			}
		}

		public static void Add<T>(this ICollection<T> collection, params T[] items)
		{
			collection.Add((IEnumerable<T>) items);
		}

		#endregion
	}
}