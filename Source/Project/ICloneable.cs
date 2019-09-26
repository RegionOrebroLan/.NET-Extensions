using System;

namespace RegionOrebroLan
{
	public interface ICloneable<out T> : ICloneable
	{
		#region Methods

		new T Clone();

		#endregion
	}
}