using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RegionOrebroLan.Collections.Generic
{
	/// <summary>
	/// A generic tree-node collection that contains no duplicate elements (tree-nodes). However It can contain multiple tree-nodes with the same value.
	/// </summary>
	/// <inheritdoc />
	[SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix")]
	public interface ITreeNodeSet<T> : IEnumerable<ITreeNode<T>>
	{
		#region Properties

		int Count { get; }

		/// <summary>
		/// Gets or sets the <see cref="ITreeNode{T}">node</see> at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the <see cref="ITreeNode{T}">node</see> to get or set.
		/// </param>
		/// <returns>
		/// The <see cref="ITreeNode{T}">node</see> at the specified index.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index">index</paramref> is not a valid index in the <see cref="ITreeNodeSet{T}">set</see>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="value">value</paramref> is null.</exception>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		ITreeNode<T> this[int index] { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Adds a <see cref="ITreeNode{T}">node</see> to the current <see cref="ITreeNodeSet{T}">set</see> and returns a value to indicate if the <see cref="ITreeNode{T}">node</see> was successfully added.
		/// </summary>
		/// <param name="node">
		/// The <see cref="ITreeNode{T}">node</see> to add to the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <returns>
		/// true if the <see cref="ITreeNode{T}">node</see> is added to the <see cref="ITreeNodeSet{T}">set</see>; false if the <see cref="ITreeNode{T}">node</see> is already in the <see cref="ITreeNodeSet{T}">set</see>.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="node">node</paramref> is null.</exception>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		bool Add(ITreeNode<T> node);

		/// <summary>
		/// Adds a <see cref="ITreeNode{T}">node</see>, with the passed value as it's value, to the current <see cref="ITreeNodeSet{T}">set</see> and returns the <see cref="ITreeNode{T}">node</see>.
		/// </summary>
		/// <param name="value">
		/// The value of the new <see cref="ITreeNode{T}">node</see>.
		/// </param>
		/// <returns>
		/// The new <see cref="ITreeNode{T}">node</see>.
		/// </returns>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		ITreeNode<T> Add(T value);

		void Clear();

		/// <summary>
		/// Determines whether the <see cref="ITreeNodeSet{T}">set</see> contains a specific <see cref="ITreeNode{T}">node</see>.
		/// </summary>
		/// <param name="node">
		/// The <see cref="ITreeNode{T}">node</see> to locate in the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <returns>
		/// true if the <see cref="ITreeNodeSet{T}">set</see> contains the <see cref="ITreeNodeSet{T}">node</see>; otherwise, false.
		/// </returns>
		bool Contains(ITreeNode<T> node);

		/// <summary>
		/// Determines whether the <see cref="ITreeNodeSet{T}">set</see> contains a <see cref="ITreeNode{T}">node</see> with a specific value.
		/// </summary>
		/// <param name="value">
		/// The value of a <see cref="ITreeNode{T}">node</see> to locate in the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <returns>
		/// true if the <see cref="ITreeNodeSet{T}">set</see> contains a <see cref="ITreeNodeSet{T}">node</see> with the specific value; otherwise, false.
		/// </returns>
		bool Contains(T value);

		/// <summary>
		/// Inserts a <see cref="ITreeNode{T}">node</see> to the current <see cref="ITreeNodeSet{T}">set</see>, at the specified index, and returns a value to indicate if the <see cref="ITreeNode{T}">node</see> was successfully inserted.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which the <see cref="ITreeNode{T}">node</see> should be inserted.
		/// </param>
		/// <param name="node">
		/// The <see cref="ITreeNode{T}">node</see> to insert to the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <returns>
		/// true if the <see cref="ITreeNode{T}">node</see> is inserted to the <see cref="ITreeNodeSet{T}">set</see>; false if the <see cref="ITreeNode{T}">node</see> is already in the <see cref="ITreeNodeSet{T}">set</see>.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index">index</paramref> is not a valid index in the <see cref="ITreeNodeSet{T}">set</see>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="node">node</paramref> is null.</exception>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		bool Insert(int index, ITreeNode<T> node);

		/// <summary>
		/// Inserts a <see cref="ITreeNode{T}">node</see>, at the specified index, with the passed value as it's value, to the current <see cref="ITreeNodeSet{T}">set</see> and returns the <see cref="ITreeNode{T}">node</see>.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which the <see cref="ITreeNode{T}">node</see> should be inserted.
		/// </param>
		/// <param name="value">
		/// The value of the new <see cref="ITreeNode{T}">node</see>.
		/// </param>
		/// <returns>
		/// The new <see cref="ITreeNode{T}">node</see>.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index">index</paramref> is not a valid index in the <see cref="ITreeNodeSet{T}">set</see>.</exception>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		ITreeNode<T> Insert(int index, T value);

		/// <summary>
		/// Removes the specific <see cref="ITreeNode{T}">node</see> from the <see cref="ITreeNodeSet{T}">set</see>.
		/// </summary>
		/// <param name="node">
		/// The <see cref="ITreeNode{T}">node</see> to remove from the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <returns>
		/// true if the <see cref="ITreeNode{T}">node</see> was successfully removed from the <see cref="ITreeNodeSet{T}">set</see>; otherwise, false. This method also returns false if the <see cref="ITreeNode{T}">node</see> is not found in the <see cref="ITreeNodeSet{T}">set</see>.
		/// </returns>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		bool Remove(ITreeNode<T> node);

		/// <summary>
		/// Removes the first occurrence of a <see cref="ITreeNode{T}">node</see> with the specific value from the <see cref="ITreeNodeSet{T}">set</see>.
		/// </summary>
		/// <param name="value">
		/// The value of the <see cref="ITreeNode{T}">node</see> to remove from the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <returns>
		/// true if a <see cref="ITreeNode{T}">node</see> with the specific <paramref name="value">value</paramref> was successfully removed from the <see cref="ITreeNodeSet{T}">set</see>; otherwise, false. This method also returns false if a <see cref="ITreeNode{T}">node</see> with the specific <paramref name="value">value</paramref> is not found in the <see cref="ITreeNodeSet{T}">set</see>.
		/// </returns>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		bool Remove(T value);

		/// <summary>
		/// Removes the <see cref="ITreeNode{T}">node</see> at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the <see cref="ITreeNode{T}">node</see> to remove.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index">index</paramref> is not a valid index in the <see cref="ITreeNodeSet{T}">set</see>.</exception>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		void RemoveAt(int index);

		/// <summary>
		/// Sorts the <see cref="ITreeNodeSet{T}">set</see>.
		/// </summary>
		/// <param name="comparer">
		/// The comparer to use for sorting the <see cref="ITreeNodeSet{T}">set</see>.
		/// </param>
		/// <exception cref="InvalidOperationException">The <see cref="ITreeNodeSet{T}">set</see> is read-only.</exception>
		void Sort(Func<ITreeNode<T>, ITreeNode<T>, int> comparer);

		#endregion
	}
}