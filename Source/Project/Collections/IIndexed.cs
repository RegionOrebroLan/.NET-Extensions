namespace RegionOrebroLan.Collections
{
	public interface IIndexed
	{
		#region Properties

		bool First { get; }
		int Index { get; }
		bool Last { get; }
		object Value { get; }

		#endregion
	}
}