using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RegionOrebroLan.Collections.Generic
{
	/// <inheritdoc cref="ITreeNodeSet{T}" />
	public class TreeNodeSet<T> : IReadOnly<TreeNodeSet<T>>, ITreeNodeSet<T>, ITreeNodeSetInternal<T>
	{
		#region Constructors

		public TreeNodeSet(ITreeNode<T> parent)
		{
			this.Parent = parent;
		}

		#endregion

		#region Properties

		public virtual int Count => this.Items.Count;
		public virtual bool IsReadOnly { get; protected internal set; }

		public virtual ITreeNode<T> this[int index]
		{
			get
			{
				this.ValidateExistingIndex(index);

				return this.Items[index];
			}
			set
			{
				this.ValidateReadOnly();

				if(value == null)
					throw new ArgumentNullException(nameof(value));

				this.ValidateExistingIndex(index);

				if(Equals(this[index], value))
					return;

				this.RemoveAt(index);

				this.Insert(index, value);
			}
		}

		protected internal virtual IList<ITreeNode<T>> Items { get; } = new List<ITreeNode<T>>();
		protected internal virtual ITreeNode<T> Parent { get; }

		#endregion

		#region Methods

		public virtual bool Add(ITreeNode<T> node)
		{
			return this.Insert(this.Items.Count, node);
		}

		public virtual ITreeNode<T> Add(T value)
		{
			return this.Insert(this.Items.Count, value);
		}

		public virtual bool Add(ITreeNode<T> node, bool resolveParent)
		{
			return this.Insert(this.Items.Count, node, resolveParent);
		}

		public virtual void Clear()
		{
			this.ValidateReadOnly();

			for(var i = this.Items.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public virtual TreeNodeSet<T> Clone()
		{
			var clone = new TreeNodeSet<T>(null);

			foreach(var node in this)
			{
				if(node is not IReadOnly readOnly)
					continue;

				clone.Add((ITreeNode<T>)readOnly.Clone());
			}

			return clone;
		}

		public virtual bool Contains(ITreeNode<T> node)
		{
			return this.Items.Contains(node);
		}

		public virtual bool Contains(T value)
		{
			return this.Items.Any(item => Equals(item.Value, value));
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual IEnumerator<ITreeNode<T>> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		protected internal virtual ITreeNodeInternal<T> GetTreeNodeInternal(ITreeNode<T> node)
		{
			try
			{
				// ReSharper disable SuspiciousTypeConversion.Global
				return (ITreeNodeInternal<T>)node;
				// ReSharper restore SuspiciousTypeConversion.Global
			}
			catch(InvalidCastException invalidCastException)
			{
				throw new InvalidOperationException($"The current implementation requires the node to implement \"{typeof(ITreeNodeInternal<T>)}\".", invalidCastException);
			}
		}

		public virtual bool Insert(int index, ITreeNode<T> node)
		{
			return this.Insert(index, node, true);
		}

		public virtual ITreeNode<T> Insert(int index, T value)
		{
			this.ValidateReadOnly();

			this.ValidateInsertIndex(index);

			var node = new TreeNode<T>
			{
				Value = value
			};

			this.Insert(index, node);

			return node;
		}

		protected internal virtual bool Insert(int index, ITreeNode<T> node, bool resolveParent)
		{
			this.ValidateReadOnly();

			if(node == null)
				throw new ArgumentNullException(nameof(node));

			this.ValidateInsertIndex(index);

			if(this.Contains(node))
				return false;

			if(resolveParent)
				this.GetTreeNodeInternal(node).SetParent(this.Parent, false);

			this.Items.Insert(index, node);

			return true;
		}

		public virtual void MakeReadOnly()
		{
			if(this.IsReadOnly)
				return;

			foreach(var item in this)
			{
				// ReSharper disable SuspiciousTypeConversion.Global
				(item as IReadOnly)?.MakeReadOnly();
				// ReSharper restore SuspiciousTypeConversion.Global
			}

			this.IsReadOnly = true;
		}

		public virtual bool Remove(ITreeNode<T> node)
		{
			return this.Remove(node, true);
		}

		public virtual bool Remove(T value)
		{
			var node = this.Items.FirstOrDefault(item => Equals(item.Value, value));

			return node != null && this.Remove(node);
		}

		public virtual bool Remove(ITreeNode<T> node, bool resolveParent)
		{
			this.ValidateReadOnly();

			var index = this.Items.IndexOf(node);

			if(index < 0)
				return false;

			this.RemoveAt(index, resolveParent);

			return true;
		}

		public virtual void RemoveAt(int index)
		{
			this.RemoveAt(index, true);
		}

		protected internal virtual void RemoveAt(int index, bool resolveParent)
		{
			this.ValidateReadOnly();

			this.ValidateExistingIndex(index);

			var node = this.Items[index];

			if(resolveParent)
				this.GetTreeNodeInternal(node).SetParent(null, false);

			this.Items.RemoveAt(index);
		}

		public virtual void Sort(Func<ITreeNode<T>, ITreeNode<T>, int> comparer)
		{
			this.ValidateReadOnly();

			if(comparer == null)
				throw new ArgumentNullException(nameof(comparer));

			var clearAndAdd = false;

			if(this.Items is not List<ITreeNode<T>> list)
			{
				clearAndAdd = true;
				list = this.Items.ToList();
			}

			list.Sort(comparer.Invoke);

			if(!clearAndAdd)
				return;

			this.Items.Clear();

			foreach(var item in list)
			{
				this.Items.Add(item);
			}
		}

		protected internal virtual void ThrowExistingIndexOutOfRangeException(int index)
		{
			this.ThrowIndexOutOfRangeException(index, "Index is out of range. The index must be non-negative and less than the size of the set.");
		}

		protected internal virtual void ThrowIndexOutOfRangeException(int index, string message)
		{
			throw new ArgumentOutOfRangeException(nameof(index), $"The index-value is \"{index}\". " + message);
		}

		protected internal virtual void ThrowInsertIndexOutOfRangeException(int index)
		{
			this.ThrowIndexOutOfRangeException(index, "Index must be within the bounds of the set.");
		}

		protected internal virtual void ThrowReadOnlyException()
		{
			throw new InvalidOperationException("The set is read-only.");
		}

		protected internal virtual void ValidateExistingIndex(int index)
		{
			if(index < 0 || index >= this.Items.Count)
				this.ThrowExistingIndexOutOfRangeException(index);
		}

		protected internal virtual void ValidateInsertIndex(int index)
		{
			if(index < 0 || index > this.Items.Count)
				this.ThrowInsertIndexOutOfRangeException(index);
		}

		protected internal virtual void ValidateReadOnly()
		{
			if(this.IsReadOnly)
				this.ThrowReadOnlyException();
		}

		#endregion
	}
}