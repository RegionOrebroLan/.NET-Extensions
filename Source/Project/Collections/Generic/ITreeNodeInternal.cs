namespace RegionOrebroLan.Collections.Generic
{
	public interface ITreeNodeInternal<T>
	{
		#region Methods

		void SetParent(ITreeNode<T> parent, bool resolveChildren);

		#endregion
	}
}