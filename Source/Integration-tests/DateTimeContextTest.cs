using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.IntegrationTests
{
	[TestClass]
	public class DateTimeContextTest
	{
		#region Methods

		[TestMethod]
		public void Now_ShouldReturnDateTimeNow()
		{
			var startDateTime = DateTime.Now;
			Thread.Sleep(1);
			var now = new DateTimeContext().Now;
			Thread.Sleep(1);
			var endDateTime = DateTime.Now;

			Assert.IsTrue(now > startDateTime && now < endDateTime);
		}

		#endregion
	}
}