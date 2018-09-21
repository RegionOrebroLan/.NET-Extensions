using System;
using System.ComponentModel.Design.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RegionOrebroLan.UnitTests
{
	[TestClass]
	public class UriTypeConverterPrerequisiteTest
	{
		#region Properties

		protected internal virtual UriTypeConverter UriTypeConverter { get; } = new UriTypeConverter();

		#endregion

		#region Methods

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsNotOfTypeUriButAssignableToUri_ShouldReturnFalse()
		{
			var customUri = new Mock<Uri>("http://localhost") {CallBase = true}.Object;
			var customUriType = customUri.GetType();

			Assert.IsFalse(typeof(Uri) == customUriType);
			Assert.IsTrue(typeof(Uri).IsAssignableFrom(customUriType));
			Assert.IsFalse(this.UriTypeConverter.CanConvertFrom(null, customUriType));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CanConvertFrom_IfTheSourceTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			this.UriTypeConverter.CanConvertFrom(null, null);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsOfTypeInstanceDescriptor_ShouldReturnFalse()
		{
			Assert.IsFalse(this.UriTypeConverter.CanConvertFrom(null, typeof(InstanceDescriptor)));
		}

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsOfTypeString_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriTypeConverter.CanConvertFrom(null, typeof(string)));
		}

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsOfTypeUri_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriTypeConverter.CanConvertFrom(null, typeof(Uri)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsNotOfTypeUriButAssignableToUri_ShouldReturnFalse()
		{
			var customUri = new Mock<Uri>("http://localhost") {CallBase = true}.Object;
			var customUriType = customUri.GetType();

			Assert.IsFalse(typeof(Uri) == customUriType);
			Assert.IsTrue(typeof(Uri).IsAssignableFrom(customUriType));
			Assert.IsFalse(this.UriTypeConverter.CanConvertTo(null, customUriType));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsNull_ShouldReturnFalse()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			this.UriTypeConverter.CanConvertTo(null, null);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsOfTypeInstanceDescriptor_ShouldReturnFalse()
		{
			Assert.IsFalse(this.UriTypeConverter.CanConvertTo(null, typeof(InstanceDescriptor)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsOfTypeString_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriTypeConverter.CanConvertTo(null, typeof(string)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsOfTypeUri_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriTypeConverter.CanConvertTo(null, typeof(Uri)));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheDestinationTypeParameterIsOfTypeInstanceDescriptorAndTheValueParameterIsAnUri_ShouldThrowANotSupportedException()
		{
			this.UriTypeConverter.ConvertTo(null, null, new Uri("http://localhost"), typeof(InstanceDescriptor));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheDestinationTypeParameterIsOfTypeStringAndTheValueParameterIsAString_ShouldThrowANotSupportedException()
		{
			this.UriTypeConverter.ConvertTo(null, null, "Value", typeof(string));
		}

		#endregion
	}
}