namespace RegionOrebroLan.Collections.Generic
{
	public interface IIndexed<out T> : IIndexed
	{
		#region Properties

		new T Value { get; }

		#endregion
	}
}