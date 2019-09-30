using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RegionOrebroLan.Collections.Generic
{
	public class CompositeComparer<T> : IComparer<T>
	{
		#region Constructors

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public CompositeComparer(IEnumerable<IComparer<T>> comparers)
		{
			var comparerList = (comparers ?? Enumerable.Empty<IComparer<T>>()).ToList();

			if(comparerList.Any(filter => filter == null))
				throw new ArgumentException("The comparer-collection can not contain null-values.", nameof(comparers));

			this.Comparers = comparerList;
		}

		public CompositeComparer(params IComparer<T>[] comparers) : this((IEnumerable<IComparer<T>>) comparers) { }

		#endregion

		#region Properties

		protected internal virtual IList<IComparer<T>> Comparers { get; }

		#endregion

		#region Methods

		public virtual int Compare(T x, T y)
		{
			var compare = 0;

			foreach(var comparer in this.Comparers)
			{
				compare = comparer.Compare(x, y);

				if(compare != 0)
					break;
			}

			return compare;
		}

		#endregion
	}
}