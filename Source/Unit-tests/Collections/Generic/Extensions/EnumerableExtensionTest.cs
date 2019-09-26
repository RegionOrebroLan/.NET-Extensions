using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.Collections.Generic;
using RegionOrebroLan.Collections.Generic.Extensions;

namespace RegionOrebroLan.UnitTests.Collections.Generic.Extensions
{
	[TestClass]
	public class EnumerableExtensionTest
	{
		#region Methods

		[TestMethod]
		public void Indexed_Test()
		{
			const string value = "Value";

			var stringList = new List<string>
			{
				value,
				value,
				value,
				value
			};

			var stringEnumerable = stringList.Indexed().ToArray();

			var stringItem = stringEnumerable.ElementAt(0);
			Assert.AreEqual(0, stringItem.Index);
			Assert.IsTrue(stringItem.First);
			Assert.IsFalse(stringItem.Last);
			Assert.AreEqual(value, stringItem.Value);
			Assert.AreEqual(typeof(Indexed<string>), stringItem.GetType());

			stringItem = stringEnumerable.ElementAt(1);
			Assert.AreEqual(1, stringItem.Index);
			Assert.IsFalse(stringItem.First);
			Assert.IsFalse(stringItem.Last);
			Assert.AreEqual(value, stringItem.Value);
			Assert.AreEqual(typeof(Indexed<string>), stringItem.GetType());

			stringItem = stringEnumerable.ElementAt(2);
			Assert.AreEqual(2, stringItem.Index);
			Assert.IsFalse(stringItem.First);
			Assert.IsFalse(stringItem.Last);
			Assert.AreEqual(value, stringItem.Value);
			Assert.AreEqual(typeof(Indexed<string>), stringItem.GetType());

			stringItem = stringEnumerable.ElementAt(3);
			Assert.AreEqual(3, stringItem.Index);
			Assert.IsFalse(stringItem.First);
			Assert.IsTrue(stringItem.Last);
			Assert.AreEqual(value, stringItem.Value);
			Assert.AreEqual(typeof(Indexed<string>), stringItem.GetType());

			var integerSet = new HashSet<int>
			{
				1,
				2,
				3,
				4
			};

			var integerEnumerable = integerSet.Indexed().ToArray();

			var integerItem = integerEnumerable.ElementAt(0);
			Assert.AreEqual(0, integerItem.Index);
			Assert.IsTrue(integerItem.First);
			Assert.IsFalse(integerItem.Last);
			Assert.AreEqual(1, integerItem.Value);
			Assert.AreEqual(typeof(Indexed<int>), integerItem.GetType());

			integerItem = integerEnumerable.ElementAt(1);
			Assert.AreEqual(1, integerItem.Index);
			Assert.IsFalse(integerItem.First);
			Assert.IsFalse(integerItem.Last);
			Assert.AreEqual(2, integerItem.Value);
			Assert.AreEqual(typeof(Indexed<int>), integerItem.GetType());

			integerItem = integerEnumerable.ElementAt(2);
			Assert.AreEqual(2, integerItem.Index);
			Assert.IsFalse(integerItem.First);
			Assert.IsFalse(integerItem.Last);
			Assert.AreEqual(3, integerItem.Value);
			Assert.AreEqual(typeof(Indexed<int>), integerItem.GetType());

			integerItem = integerEnumerable.ElementAt(3);
			Assert.AreEqual(3, integerItem.Index);
			Assert.IsFalse(integerItem.First);
			Assert.IsTrue(integerItem.Last);
			Assert.AreEqual(4, integerItem.Value);
			Assert.AreEqual(typeof(Indexed<int>), integerItem.GetType());
		}

		#endregion
	}
}