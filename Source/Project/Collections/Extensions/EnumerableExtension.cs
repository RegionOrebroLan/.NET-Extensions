using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RegionOrebroLan.Collections.Extensions
{
	public static class EnumerableExtension
	{
		#region Methods

		public static IEnumerable<IIndexed> Indexed(this IEnumerable enumerable)
		{
			return enumerable.IndexedInternal<Indexed, object>((index, value) => new Indexed(index, value), () => new List<Indexed>());
		}

		internal static IEnumerable<TIndexed> IndexedInternal<TIndexed, TValue>(this IEnumerable enumerable, Func<int, TValue, TIndexed> itemFactory, Func<IList<TIndexed>> listFactory) where TIndexed : Indexed
		{
			if(enumerable == null)
				throw new ArgumentNullException(nameof(enumerable));

			if(itemFactory == null)
				throw new ArgumentNullException(nameof(itemFactory));

			if(listFactory == null)
				throw new ArgumentNullException(nameof(listFactory));

			var index = 0;
			var indexed = listFactory();

			foreach(var item in enumerable)
			{
				indexed.Add(itemFactory(index, (TValue) item));
				index++;
			}

			var last = indexed.LastOrDefault();

			if(last != null)
				last.Last = true;

			return indexed;
		}

		#endregion
	}
}