using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.Collections.Generic;

namespace RegionOrebroLan.UnitTests.Collections.Generic
{
	[TestClass]
	public class TreeNodeSetTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Add_WithObjectParameter_IfTheNodeSetIsReadOnly_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNodeSet<object>(Mock.Of<ITreeNode<object>>()) {IsReadOnly = true}.Add((object) null);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsReadOnlyException(invalidOperationException))
					throw;
			}
		}

		[TestMethod]
		public void Add_WithObjectParameter_ShouldAcceptNullValue()
		{
			Assert.IsNull(new TreeNodeSet<object>(Mock.Of<ITreeNode<object>>()).Add((object) null).Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Add_WithTreeNodeParameter_IfTheNodeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new TreeNodeSet<object>(Mock.Of<ITreeNode<object>>()).Add(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName.Equals("node", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Add_WithTreeNodeParameter_IfTheNodeSetIsReadOnly_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNodeSet<object>(Mock.Of<ITreeNode<object>>()) {IsReadOnly = true}.Add(null);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsReadOnlyException(invalidOperationException))
					throw;
			}
		}

		[TestMethod]
		public void Constructor_ShouldSetTheParentProperty()
		{
			var treeNode = Mock.Of<ITreeNode<object>>();

			Assert.IsTrue(ReferenceEquals(treeNode, new TreeNodeSet<object>(treeNode).Parent));
		}

		[TestMethod]
		[SuppressMessage("Usage", "CA1806:Do not ignore method results")]
		public void Constructor_WithParentParameter_ShouldAllowNull()
		{
			// ReSharper disable ObjectCreationAsStatement
			new TreeNodeSet<object>(null);
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetTreeNodeInternal_IfTheNodeParameterDoesNotImplementTreeNodeInternal_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new TreeNodeSet<object>(Mock.Of<ITreeNode<object>>()).GetTreeNodeInternal(Mock.Of<ITreeNode<object>>());
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(invalidOperationException.Message.Equals($"The current implementation requires the node to implement \"{typeof(ITreeNodeInternal<object>)}\".", StringComparison.Ordinal) && invalidOperationException.InnerException is InvalidCastException)
					throw;
			}
		}

		protected internal virtual bool IsReadOnlyException(InvalidOperationException invalidOperationException)
		{
			return invalidOperationException != null && invalidOperationException.Message.Equals("The set is read-only.", StringComparison.Ordinal);
		}

		[TestMethod]
		public void Sort_Test()
		{
			var treeNodeSet = new TreeNodeSet<int>(null)
			{
				5,
				7,
				2,
				9,
				1,
				17,
				4
			};

			treeNodeSet.Sort((firstNode, secondNode) => firstNode.Value.CompareTo(secondNode.Value));

			Assert.AreEqual(1, treeNodeSet[0].Value);
			Assert.AreEqual(2, treeNodeSet[1].Value);
			Assert.AreEqual(4, treeNodeSet[2].Value);
			Assert.AreEqual(5, treeNodeSet[3].Value);
			Assert.AreEqual(7, treeNodeSet[4].Value);
			Assert.AreEqual(9, treeNodeSet[5].Value);
			Assert.AreEqual(17, treeNodeSet[6].Value);
		}

		#endregion
	}
}