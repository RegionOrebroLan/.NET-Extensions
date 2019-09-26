using System;
using System.Diagnostics.CodeAnalysis;

namespace RegionOrebroLan.Collections.Generic
{
	/// <inheritdoc cref="ITreeNode{T}" />
	public class TreeNode<T> : IReadOnly<TreeNode<T>>, ITreeNode<T>, ITreeNodeInternal<T>
	{
		#region Fields

		private ITreeNodeSet<T> _childrenInternal;

		#endregion

		#region Properties

		public virtual ITreeNodeSet<T> Children => this.ChildrenInternal;

		protected internal virtual ITreeNodeSet<T> ChildrenInternal
		{
			get => this._childrenInternal ?? (this._childrenInternal = new TreeNodeSet<T>(this));
			set => this._childrenInternal = value;
		}

		public virtual bool IsReadOnly { get; protected internal set; }

		public virtual ITreeNode<T> Parent
		{
			get => this.ParentInternal;
			set => this.SetParent(value, true);
		}

		protected internal virtual ITreeNode<T> ParentInternal { get; set; }

		public virtual T Value
		{
			get => this.ValueInternal;
			set
			{
				this.ValidateReadOnly();
				this.ValueInternal = value;
			}
		}

		protected internal virtual T ValueInternal { get; set; }

		#endregion

		#region Methods

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public virtual TreeNode<T> Clone()
		{
			var clone = (TreeNode<T>) this.MemberwiseClone();

			clone.IsReadOnly = false;

			clone.ParentInternal = null;

			clone.ChildrenInternal = new TreeNodeSet<T>(clone);

			foreach(var child in (this.Children as IReadOnly)?.Clone() as ITreeNodeSet<T> ?? new TreeNodeSet<T>(null))
			{
				clone.Children.Add(child);
			}

			return clone;
		}

		protected internal virtual ITreeNodeSetInternal<T> GetTreeNodeSetInternal(ITreeNodeSet<T> nodeSet)
		{
			try
			{
				return (ITreeNodeSetInternal<T>) nodeSet;
			}
			catch(InvalidCastException invalidCastException)
			{
				throw new InvalidOperationException($"The current implementation requires the node-set to implement \"{typeof(ITreeNodeSetInternal<T>)}\".", invalidCastException);
			}
		}

		public virtual void MakeReadOnly()
		{
			if(this.IsReadOnly)
				return;

			(this.Children as IReadOnly)?.MakeReadOnly();

			this.IsReadOnly = true;
		}

		protected internal virtual void ResolveNewParent(ITreeNode<T> newParent, bool resolveChildren)
		{
			if(newParent == null)
				return;

			if(!resolveChildren)
				return;

			this.GetTreeNodeSetInternal(newParent.Children).Add(this, false);
		}

		protected internal virtual void ResolvePreviousParent(ITreeNode<T> previousParent)
		{
			if(previousParent == null)
				return;

			if(!previousParent.Children.Contains(this))
				return;

			this.GetTreeNodeSetInternal(previousParent.Children).Remove(this, false);
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual void SetParent(ITreeNode<T> parent, bool resolveChildren)
		{
			// Ideas: https://github.com/SharePoint/PnP-Sites-Core/blob/master/Core/OfficeDevPnP.Core/Diagnostics/Tree/TreeNode.cs#L71

			this.ValidateReadOnly();

			if(this.ParentInternal == parent)
				return;

			if(this == parent)
				throw new ArgumentException("The parent can not be the node itself.", nameof(parent));

			this.ResolvePreviousParent(this.ParentInternal);

			this.ParentInternal = parent;

			this.ResolveNewParent(this.ParentInternal, resolveChildren);
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		protected internal virtual void ThrowReadOnlyException()
		{
			throw new InvalidOperationException("The node is read-only.");
		}

		protected internal virtual void ValidateReadOnly()
		{
			if(this.IsReadOnly)
				this.ThrowReadOnlyException();
		}

		#endregion
	}
}