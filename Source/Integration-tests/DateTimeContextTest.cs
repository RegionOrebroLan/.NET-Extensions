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
		public void Now_ShouldReturnADateTimeWithAKindThatIsLocal()
		{
			Assert.AreEqual(DateTimeKind.Local, new DateTimeContext().Now.Kind);
		}

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

		[TestMethod]
		public void UtcNow_ShouldReturnADateTimeWithAKindThatIsUtc()
		{
			Assert.AreEqual(DateTimeKind.Utc, new DateTimeContext().UtcNow.Kind);
		}

		[TestMethod]
		public void UtcNow_ShouldReturnDateTimeUtcNow()
		{
			var startDateTime = DateTime.UtcNow;
			Thread.Sleep(1);
			var utcNow = new DateTimeContext().UtcNow;
			Thread.Sleep(1);
			var endDateTime = DateTime.UtcNow;

			Assert.IsTrue(utcNow > startDateTime && utcNow < endDateTime);
		}

		#endregion
	}
}