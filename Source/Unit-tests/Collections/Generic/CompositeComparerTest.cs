using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.Collections.Generic;

namespace RegionOrebroLan.UnitTests.Collections.Generic
{
	[TestClass]
	public class CompositeComparerTest
	{
		#region Methods

		[TestMethod]
		public void Compare_IfOneComparerReturnNotZero_TheNextComparerShouldNotBeCalled()
		{
			var returnValue = -1;
			var compositeComparer = new CompositeComparer<object>(this.CreateComparer<object>(returnValue), this.CreateComparer<object>(Times.Never()));

			Assert.AreEqual(returnValue, compositeComparer.Compare(new object(), new object()));

			returnValue = -10;
			compositeComparer = new CompositeComparer<object>(this.CreateComparer<object>(returnValue), this.CreateComparer<object>(Times.Never()));

			Assert.AreEqual(returnValue, compositeComparer.Compare(new object(), new object()));

			returnValue = 1;
			compositeComparer = new CompositeComparer<object>(this.CreateComparer<object>(returnValue), this.CreateComparer<object>(Times.Never()));

			Assert.AreEqual(returnValue, compositeComparer.Compare(new object(), new object()));

			returnValue = 70;
			compositeComparer = new CompositeComparer<object>(this.CreateComparer<object>(returnValue), this.CreateComparer<object>(Times.Never()));

			Assert.AreEqual(returnValue, compositeComparer.Compare(new object(), new object()));
		}

		protected internal virtual IComparer<T> CreateComparer<T>(int returnValue)
		{
			var comparerMock = new Mock<IComparer<T>>();
			comparerMock.Setup(comparer => comparer.Compare(It.IsAny<T>(), It.IsAny<T>())).Returns(returnValue);

			return comparerMock.Object;
		}

		protected internal virtual IComparer<T> CreateComparer<T>(Times times)
		{
			var comparerMock = new Mock<IComparer<T>>();
			comparerMock.Verify(comparer => comparer.Compare(It.IsAny<T>(), It.IsAny<T>()), times);

			return comparerMock.Object;
		}

		#endregion
	}
}