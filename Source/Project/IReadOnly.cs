using System;

namespace RegionOrebroLan
{
	public interface IReadOnly : ICloneable
	{
		#region Properties

		bool IsReadOnly { get; }

		#endregion

		#region Methods

		void MakeReadOnly();

		#endregion
	}

	public interface IReadOnly<out T> : ICloneable<T>, IReadOnly { }
}