namespace RegionOrebroLan.Collections.Generic
{
	public interface ITreeNodeSetInternal<T>
	{
		#region Methods

		bool Add(ITreeNode<T> node, bool resolveParent);
		bool Remove(ITreeNode<T> node, bool resolveParent);

		#endregion
	}
}