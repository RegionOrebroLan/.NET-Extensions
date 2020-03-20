using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests.Collections.Generic
{
	[TestClass]
	public class PrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void HashSet_Add_Test()
		{
			var set = new HashSet<object>();

			var value = new object();

			Assert.IsTrue(set.Add(value));

			Assert.AreEqual(1, set.Count);

			Assert.IsFalse(set.Add(value));

			Assert.AreEqual(1, set.Count);
		}

		[TestMethod]
		[SuppressMessage("Design", "CA1031:Do not catch general exception types")]
		public void List_ArgumentOutOfRangeException_Test()
		{
			var exceptionMessages = new List<string>();

			// ReSharper disable CollectionNeverQueried.Local
			var list = new List<object>();
			// ReSharper restore CollectionNeverQueried.Local

			try
			{
				list.Insert(1, null);
			}
			catch(Exception exception)
			{
				exceptionMessages.Add("Insert: " + exception);
			}

			try
			{
				list[1] = null;
			}
			catch(Exception exception)
			{
				exceptionMessages.Add("Item: " + exception);
			}

			try
			{
				list.RemoveAt(1);
			}
			catch(Exception exception)
			{
				exceptionMessages.Add("RemoveAt: " + exception);
			}

			var toLookAt = string.Join(Environment.NewLine + Environment.NewLine, exceptionMessages);

			Assert.AreEqual(3, exceptionMessages.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_Insert_IfTheIndexParameterIsLargerThanTheListSize_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			// ReSharper disable CollectionNeverQueried.Local
			var list = new List<int> {1, 2, 3};
			// ReSharper restore CollectionNeverQueried.Local

			list.Insert(-4, 4);
		}

		[TestMethod]
		public void List_Insert_IfTheIndexParameterIsTheSameAsTheListSize_ShouldInsertTheItemLast()
		{
			var list = new List<int> {1, 2, 3};

			list.Insert(3, 4);

			Assert.AreEqual(4, list.Last());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_Item_Get_IfTheIndexIsOutOfRange_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			var list = new List<int> {1, 2, 3};

			// ReSharper disable UnusedVariable
			var item = list[3];
			// ReSharper restore UnusedVariable
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_Item_Set_IfTheIndexIsOutOfRange_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			new List<int>()[0] = 1;
		}

		[TestMethod]
		public void List_Remove_RemovesTheFirstOccurrenceOfASpecificObjectFromTheList()
		{
			var value = new object();

			var list = new List<object>
			{
				value,
				value,
				value
			};

			Assert.AreEqual(3, list.Count);

			Assert.IsTrue(list.Remove(value));

			Assert.AreEqual(2, list.Count);

			Assert.IsTrue(list.Remove(value));

			Assert.AreEqual(1, list.Count);

			Assert.IsTrue(list.Remove(value));

			Assert.IsFalse(list.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_RemoveAt_IfTheIndexParameterIsLargerThanTheListSize_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			new List<int> {1, 2, 3}.RemoveAt(4);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_RemoveAt_IfTheIndexParameterIsMinusOne_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			new List<int> {1, 2, 3}.RemoveAt(-1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_RemoveAt_IfTheIndexParameterIsTheSameAsTheListSize_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			new List<int> {1, 2, 3}.RemoveAt(3);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void List_RemoveAt_IfTheIndexParameterIsZeroAndTheListIsEmpty_ShouldThrowAnArgumentOutOfRangeExcepton()
		{
			new List<string>().RemoveAt(0);
		}

		#endregion
	}
}