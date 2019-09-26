using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.Collections.Generic;

namespace RegionOrebroLan.UnitTests.Collections.Generic
{
	[TestClass]
	public class TreeNodeTest
	{
		#region Methods

		[TestMethod]
		public void Children_ShouldReturnAnEmptyTreeNodeSetByDefault()
		{
			var treeNode = new TreeNode<object>();
			Assert.IsFalse(treeNode.Children.Any());
			Assert.AreEqual(typeof(TreeNodeSet<object>), treeNode.Children.GetType());
		}

		[TestMethod]
		public void Clone_ShouldReturnANodeWithAParentThatIsNull()
		{
			Assert.IsNull(new TreeNode<object> {Parent = new TreeNode<object>()}.Clone().Parent);
		}

		[TestMethod]
		public void Clone_ShouldReturnAWritableNode()
		{
			Assert.IsFalse(new TreeNode<object> {IsReadOnly = true}.Clone().IsReadOnly);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetTreeNodeSetInternal_IfTheNodeParameterDoesNotImplementTreeNodeSetInternal_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNode<object>().GetTreeNodeSetInternal(Mock.Of<ITreeNodeSet<object>>());
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(invalidOperationException.Message.Equals($"The current implementation requires the node-set to implement \"{typeof(ITreeNodeSetInternal<object>)}\".", StringComparison.Ordinal) && invalidOperationException.InnerException is InvalidCastException)
					throw;
			}
		}

		[TestMethod]
		public void IsReadOnly_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new TreeNode<object>().IsReadOnly);
		}

		protected internal virtual bool IsReadOnlyException(InvalidOperationException invalidOperationException)
		{
			return invalidOperationException != null && invalidOperationException.Message.Equals("The node is read-only.", StringComparison.Ordinal);
		}

		[TestMethod]
		public void MakeReadOnly_ShouldMakeTheNodeReadOnly()
		{
			var node = new TreeNode<object>();
			node.MakeReadOnly();
			Assert.IsTrue(node.IsReadOnly);
		}

		[TestMethod]
		public void Parent_Get_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new TreeNode<object>().Parent);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Parent_Set_IfTheNodeIsReadOnly_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNode<object> {IsReadOnly = true}.Parent = null;
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsReadOnlyException(invalidOperationException))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Parent_Set_IfTheValueIsTheNodeItself_ShouldThrowAnArgumentException()
		{
			var treeNode = new TreeNode<object>();

			try
			{
				treeNode.Parent = treeNode;
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.StartsWith("The parent can not be the node itself.", StringComparison.Ordinal) && argumentException.ParamName.Equals("parent", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void Parent_Set_ShouldAllowNull()
		{
			Assert.IsNull(new TreeNode<object> {Parent = null}.Parent);
		}

		[TestMethod]
		public void Parent_Set_ShouldSetTheParentProperty()
		{
			var parent = new TreeNode<object>();

			Assert.AreEqual(parent, new TreeNode<object> {Parent = parent}.Parent);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SetParent_IfTheNodeIsReadOnly_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNode<object> {IsReadOnly = true}.SetParent(null, false);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsReadOnlyException(invalidOperationException))
					throw;
			}
		}

		[TestMethod]
		public void Value_Get_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new TreeNode<object>().Value);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Value_Set_IfTheNodeIsReadOnly_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNode<object> {IsReadOnly = true}.Value = null;
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsReadOnlyException(invalidOperationException))
					throw;
			}
		}

		[TestMethod]
		public void Value_Set_ShouldAllowNull()
		{
			Assert.IsNull(new TreeNode<object> {Value = null}.Value);
		}

		[TestMethod]
		public void Value_Set_ShouldSetTheValueProperty()
		{
			const int number = 7;
			Assert.AreEqual(number, new TreeNode<object> {Value = number}.Value);
			Assert.AreEqual(number, new TreeNode<int> {Value = number}.Value);

			const string text = "Test";
			Assert.AreEqual(text, new TreeNode<object> {Value = text}.Value);
			Assert.AreEqual(text, new TreeNode<string> {Value = text}.Value);

			object value = new object();
			Assert.AreEqual(value, new TreeNode<object> {Value = value}.Value);
		}

		#endregion
	}
}