using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Collections.Generic;

namespace RegionOrebroLan.IntegrationTests.Collections.Generic
{
	[TestClass]
	public class TreeNodeSetTest
	{
		#region Methods

		[TestMethod]
		public void Add_WithObjectParameter_ShouldSetTheParentForTheAddedNode()
		{
			var parent = new TreeNode<object>();
			var child = parent.Children.Add(1);

			Assert.AreEqual(parent, child.Parent);
		}

		[TestMethod]
		public void Add_WithTreeNodeParameter_ShouldSetTheParentForTheAddedNode()
		{
			var parent = new TreeNode<object>();
			var child = new TreeNode<object>();

			parent.Children.Add(child);

			Assert.AreEqual(parent, child.Parent);
		}

		#endregion
	}
}