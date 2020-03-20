using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Collections;
using RegionOrebroLan.Collections.Extensions;

namespace RegionOrebroLan.UnitTests.Collections.Extensions
{
	[TestClass]
	public class EnumerableExtensionTest
	{
		#region Methods

		[TestMethod]
		public void Indexed_Test()
		{
			const string value = "Value";

			var list = new List<string>
			{
				value,
				value,
				value,
				value
			};

			var enumerable = list.Indexed().ToArray();

			var item = enumerable.ElementAt(0);
			Assert.AreEqual(0, item.Index);
			Assert.IsTrue(item.First);
			Assert.IsFalse(item.Last);
			Assert.AreEqual(value, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			item = enumerable.ElementAt(1);
			Assert.AreEqual(1, item.Index);
			Assert.IsFalse(item.First);
			Assert.IsFalse(item.Last);
			Assert.AreEqual(value, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			item = enumerable.ElementAt(2);
			Assert.AreEqual(2, item.Index);
			Assert.IsFalse(item.First);
			Assert.IsFalse(item.Last);
			Assert.AreEqual(value, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			item = enumerable.ElementAt(3);
			Assert.AreEqual(3, item.Index);
			Assert.IsFalse(item.First);
			Assert.IsTrue(item.Last);
			Assert.AreEqual(value, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			var set = new HashSet<int>
			{
				1,
				2,
				3,
				4
			};

			enumerable = set.Indexed().ToArray();

			item = enumerable.ElementAt(0);
			Assert.AreEqual(0, item.Index);
			Assert.IsTrue(item.First);
			Assert.IsFalse(item.Last);
			Assert.AreEqual(1, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			item = enumerable.ElementAt(1);
			Assert.AreEqual(1, item.Index);
			Assert.IsFalse(item.First);
			Assert.IsFalse(item.Last);
			Assert.AreEqual(2, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			item = enumerable.ElementAt(2);
			Assert.AreEqual(2, item.Index);
			Assert.IsFalse(item.First);
			Assert.IsFalse(item.Last);
			Assert.AreEqual(3, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());

			item = enumerable.ElementAt(3);
			Assert.AreEqual(3, item.Index);
			Assert.IsFalse(item.First);
			Assert.IsTrue(item.Last);
			Assert.AreEqual(4, item.Value);
			Assert.AreEqual(typeof(Indexed), item.GetType());
		}

		#endregion
	}
}