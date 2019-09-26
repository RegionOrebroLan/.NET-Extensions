using System;
using System.Collections.Generic;

namespace RegionOrebroLan.Collections.Generic.Extensions
{
	public static class TreeNodeExtension
	{
		#region Methods

		public static IEnumerable<ITreeNode<T>> Ancestors<T>(this ITreeNode<T> node)
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			while(node.Parent != null)
			{
				yield return node.Parent;

				node = node.Parent;
			}
		}

		public static IEnumerable<ITreeNode<T>> Descendants<T>(this ITreeNode<T> node)
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			foreach(var child in node.Children)
			{
				yield return child;

				foreach(var descendant in child.Descendants())
				{
					yield return descendant;
				}
			}
		}

		public static int Level<T>(this ITreeNode<T> node)
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			var level = 0;

			while(node.Parent != null)
			{
				level++;

				node = node.Parent;
			}

			return level;
		}

		public static ITreeNode<T> Root<T>(this ITreeNode<T> node)
		{
			if(node == null)
				throw new ArgumentNullException(nameof(node));

			while(node.Parent != null)
			{
				node = node.Parent;
			}

			return node;
		}

		#endregion
	}
}