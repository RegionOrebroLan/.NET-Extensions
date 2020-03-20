using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Collections.Generic;

namespace RegionOrebroLan.IntegrationTests.Collections.Generic
{
	[TestClass]
	public class TreeNodeTest
	{
		#region Methods

		[TestMethod]
		public void Clone_ShouldLeaveTheOriginalIntact()
		{
			var parent = new TreeNode<object>();
			var node = new TreeNode<object> {Parent = parent, Value = "Test"};
			node.Children.Add(1);
			node.Children.Add(2);
			node.Children.Add(3);
			node.Children.Add(4);
			node.MakeReadOnly();

			node.Clone();

			Assert.AreEqual(parent, node.Parent);
			Assert.AreEqual(4, node.Children.Count);
			Assert.AreEqual(1, node.Children[0].Value);
			Assert.AreEqual(2, node.Children[1].Value);
			Assert.AreEqual(3, node.Children[2].Value);
			Assert.AreEqual(4, node.Children[3].Value);
		}

		[TestMethod]
		public void Clone_ShouldWorkProperly()
		{
			var node = new TreeNode<object> {Parent = new TreeNode<object>(), Value = "Test"};
			node.Children.Add(1);
			node.Children.Add(2);
			node.Children.Add(3);
			node.Children.Add(4);
			node.MakeReadOnly();

			var clone = node.Clone();

			Assert.AreNotEqual(node, clone);
			Assert.IsNull(clone.Parent);
			Assert.AreEqual("Test", clone.Value);
			Assert.AreEqual(clone, ((TreeNodeSet<object>) clone.Children).Parent);

			Assert.AreNotEqual(node.Children[0], clone.Children[0]);
			Assert.AreNotEqual(node.Children[1], clone.Children[1]);
			Assert.AreNotEqual(node.Children[2], clone.Children[2]);
			Assert.AreNotEqual(node.Children[3], clone.Children[3]);

			Assert.AreEqual(1, clone.Children[0].Value);
			Assert.AreEqual(2, clone.Children[1].Value);
			Assert.AreEqual(3, clone.Children[2].Value);
			Assert.AreEqual(4, clone.Children[3].Value);
		}

		[TestMethod]
		public void MakeReadOnly_ShouldMakeChildrenReadOnly()
		{
			var node = new TreeNode<object>();
			node.Children.Add(1);
			node.Children.Add(2);
			node.Children.Add(3);

			node.MakeReadOnly();

			Assert.IsTrue(node.IsReadOnly);
			Assert.IsTrue(((TreeNodeSet<object>) node.Children).IsReadOnly);
			Assert.IsTrue(((TreeNode<object>) node.Children[0]).IsReadOnly);
			Assert.IsTrue(((TreeNode<object>) node.Children[1]).IsReadOnly);
			Assert.IsTrue(((TreeNode<object>) node.Children[2]).IsReadOnly);
		}

		[TestMethod]
		public void Parent_Set_ShouldAddTheNodeAsChildInParent()
		{
			var parent = new TreeNode<object>();
			var child = new TreeNode<object>
			{
				Parent = parent
			};

			Assert.IsTrue(parent.Children.Contains(child));
		}

		[TestMethod]
		public void Parent_Set_ShouldRemoveTheNodeAsChildInPreviousParent()
		{
			var parent = new TreeNode<object>();
			var child = new TreeNode<object>();

			parent.Children.Add(child);

			child.Parent = new TreeNode<object>();

			Assert.IsFalse(parent.Children.Contains(child));
		}

		#endregion
	}
}