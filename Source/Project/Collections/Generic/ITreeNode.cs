namespace RegionOrebroLan.Collections.Generic
{
	public interface ITreeNode<T>
	{
		#region Properties

		ITreeNodeSet<T> Children { get; }
		ITreeNode<T> Parent { get; set; }
		T Value { get; set; }

		#endregion
	}
}