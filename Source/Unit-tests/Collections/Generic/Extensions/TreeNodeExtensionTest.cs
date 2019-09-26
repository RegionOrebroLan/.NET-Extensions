using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Collections.Generic;
using RegionOrebroLan.Collections.Generic.Extensions;

namespace RegionOrebroLan.UnitTests.Collections.Generic.Extensions
{
	[TestClass]
	public class TreeNodeExtensionTest
	{
		#region Methods

		[TestMethod]
		public void Ancestors_IfTheParentIsNull_ShouldReturnAnEmptyCollection()
		{
			Assert.IsFalse(new TreeNode<object>().Ancestors().Any());
		}

		[TestMethod]
		public void Ancestors_Test()
		{
			ITreeNode<object> treeNode = new TreeNode<object>();

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(1, treeNode.Ancestors().Count());
			Assert.AreEqual(treeNode.Parent, treeNode.Ancestors().ElementAt(0));

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(2, treeNode.Ancestors().Count());
			Assert.AreEqual(treeNode.Parent, treeNode.Ancestors().ElementAt(0));
			Assert.AreEqual(treeNode.Parent.Parent, treeNode.Ancestors().ElementAt(1));

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(3, treeNode.Ancestors().Count());
			Assert.AreEqual(treeNode.Parent, treeNode.Ancestors().ElementAt(0));
			Assert.AreEqual(treeNode.Parent.Parent, treeNode.Ancestors().ElementAt(1));
			Assert.AreEqual(treeNode.Parent.Parent.Parent, treeNode.Ancestors().ElementAt(2));

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(4, treeNode.Ancestors().Count());
			Assert.AreEqual(treeNode.Parent, treeNode.Ancestors().ElementAt(0));
			Assert.AreEqual(treeNode.Parent.Parent, treeNode.Ancestors().ElementAt(1));
			Assert.AreEqual(treeNode.Parent.Parent.Parent, treeNode.Ancestors().ElementAt(2));
			Assert.AreEqual(treeNode.Parent.Parent.Parent.Parent, treeNode.Ancestors().ElementAt(3));

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(5, treeNode.Ancestors().Count());
			Assert.AreEqual(treeNode.Parent, treeNode.Ancestors().ElementAt(0));
			Assert.AreEqual(treeNode.Parent.Parent, treeNode.Ancestors().ElementAt(1));
			Assert.AreEqual(treeNode.Parent.Parent.Parent, treeNode.Ancestors().ElementAt(2));
			Assert.AreEqual(treeNode.Parent.Parent.Parent.Parent, treeNode.Ancestors().ElementAt(3));
			Assert.AreEqual(treeNode.Parent.Parent.Parent.Parent.Parent, treeNode.Ancestors().ElementAt(4));
		}

		[TestMethod]
		public void Descendants_IfTheNodeHasNoChildren_ShouldReturnAnEmptyCollection()
		{
			Assert.IsFalse(new TreeNode<object>().Descendants().Any());
		}

		[TestMethod]
		public void Descendants_Test()
		{
			var root = new TreeNode<string>();

			this.PopulateTree(5, 0, root, 5);

			root.Value = "Root";

			Assert.AreEqual("1", root.Descendants().ElementAt(0).Value);
			Assert.AreEqual("1.1", root.Descendants().ElementAt(1).Value);
			Assert.AreEqual("1.1.1", root.Descendants().ElementAt(2).Value);
			Assert.AreEqual("1.1.1.1", root.Descendants().ElementAt(3).Value);
			Assert.AreEqual("1.1.1.1.1", root.Descendants().ElementAt(4).Value);
			Assert.AreEqual("1.1.1.1.2", root.Descendants().ElementAt(5).Value);
			Assert.AreEqual("1.1.1.1.3", root.Descendants().ElementAt(6).Value);
			Assert.AreEqual("1.1.1.1.4", root.Descendants().ElementAt(7).Value);
			Assert.AreEqual("1.1.1.1.5", root.Descendants().ElementAt(8).Value);
			Assert.AreEqual("1.1.1.2", root.Descendants().ElementAt(9).Value);

			Assert.AreEqual("5.5.5.5.5", root.Descendants().Last().Value);

			Assert.IsFalse(root.Descendants().Any(node => node.Value.Contains("0")));
			Assert.IsFalse(root.Descendants().Any(node => node.Value.Contains("6")));
			Assert.IsFalse(root.Descendants().Any(node => node.Value.Contains("7")));
			Assert.IsFalse(root.Descendants().Any(node => node.Value.Contains("8")));
			Assert.IsFalse(root.Descendants().Any(node => node.Value.Contains("9")));

			Assert.AreEqual(9, root.Descendants().Select(node => node.Value.Length).Max());

			var distinctCharacters = root.Descendants().SelectMany(node => node.Value.ToCharArray()).Distinct().OrderBy(character => character).ToArray();

			Assert.AreEqual(6, distinctCharacters.Count());

			Assert.AreEqual(root.Descendants().Count(), root.Descendants().Distinct().Count());

			Assert.AreEqual(3905, root.Descendants().Count());
		}

		[TestMethod]
		public void Level_IfTheParentIsNull_ShouldReturnZero()
		{
			Assert.AreEqual(0, new TreeNode<object>().Level());
		}

		[TestMethod]
		public void Level_Test()
		{
			ITreeNode<object> treeNode = new TreeNode<object>();

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(1, treeNode.Level());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(2, treeNode.Level());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(3, treeNode.Level());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(4, treeNode.Level());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(5, treeNode.Level());
		}

		protected internal virtual void PopulateTree(int depth, int level, ITreeNode<string> parent, int width)
		{
			if(parent == null)
				throw new ArgumentNullException(nameof(parent));

			if(level >= depth)
				return;

			level++;

			for(var i = 1; i <= width; i++)
			{
				var value = parent.Value + (!string.IsNullOrEmpty(parent.Value) ? "." : string.Empty) + i;

				this.PopulateTree(depth, level, parent.Children.Add(value), width);
			}
		}

		[TestMethod]
		public void Root_IfTheParentIsNull_ShouldReturnItself()
		{
			var treeNode = new TreeNode<object>();
			Assert.AreEqual(treeNode, treeNode.Root());
		}

		[TestMethod]
		public void Root_Test()
		{
			var root = new TreeNode<object>();

			var treeNode = root.Children.Add(new object());
			Assert.AreEqual(root, treeNode.Root());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(root, treeNode.Root());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(root, treeNode.Root());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(root, treeNode.Root());

			treeNode = treeNode.Children.Add(new object());
			Assert.AreEqual(root, treeNode.Root());
		}

		#endregion
	}
}