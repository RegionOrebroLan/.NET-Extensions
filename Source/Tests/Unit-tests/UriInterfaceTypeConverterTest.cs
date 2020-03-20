using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RegionOrebroLan.UnitTests
{
	[TestClass]
	public class UriInterfaceTypeConverterTest
	{
		#region Properties

		protected internal virtual UriInterfaceTypeConverter UriInterfaceTypeConverter { get; } = new UriInterfaceTypeConverter();

		#endregion

		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CanConvertFrom_IfTheSourceTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			this.UriInterfaceTypeConverter.CanConvertFrom(null, null);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsOfTypeString_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.CanConvertFrom(null, typeof(string)));
		}

		[TestMethod]
		public void CanConvertFrom_IfUriInterfaceIsAssignableFromTheSourceTypeParameter_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.CanConvertFrom(null, typeof(IUri)));
		}

		[TestMethod]
		public void CanConvertFrom_IfUriIsAssignableFromTheSourceTypeParameter_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.CanConvertFrom(null, typeof(Uri)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsNull_ShouldReturnFalse()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			Assert.IsFalse(this.UriInterfaceTypeConverter.CanConvertTo(null, null));
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParamterIsOfTypeString_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.CanConvertTo(null, typeof(string)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParamterIsOfTypeUri_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.CanConvertTo(null, typeof(Uri)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParamterIsOfTypeUriWrapper_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.CanConvertTo(null, typeof(UriWrapper)));
		}

		[TestMethod]
		public void ConvertFrom_IfTheValueParameterIsAConcreteUri_ShouldReturnAnUriWrapper()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			Assert.IsTrue(this.UriInterfaceTypeConverter.ConvertFrom(null, null, new Uri("http://localhost")) is UriWrapper);
			Assert.IsTrue(this.UriInterfaceTypeConverter.ConvertFrom(null, null, new Uri("Directory", UriKind.RelativeOrAbsolute)) is UriWrapper);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		public void ConvertFrom_IfTheValueParameterIsAnUri_ShouldReturnAnUriWrapper()
		{
			// ReSharper disable AssignNullToNotNullAttribute

			var uriMock = new Mock<IUri>();
			uriMock.Setup(uri => uri.OriginalString).Returns("http://localhost");
			Assert.IsTrue(this.UriInterfaceTypeConverter.ConvertFrom(null, null, uriMock.Object) is UriWrapper);

			uriMock = new Mock<IUri>();
			uriMock.Setup(uri => uri.OriginalString).Returns("Directory");
			Assert.IsTrue(this.UriInterfaceTypeConverter.ConvertFrom(null, null, uriMock.Object) is UriWrapper);

			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		public void ConvertFrom_IfTheValueParameterIsAValidUriString_ShouldReturnAnUriWrapper()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			Assert.IsTrue(this.UriInterfaceTypeConverter.ConvertFrom(null, null, "2") is UriWrapper);
			Assert.IsTrue(this.UriInterfaceTypeConverter.ConvertFrom(null, null, "Directory") is UriWrapper);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertFrom_IfTheValueParameterIsNull_ShouldThrowANotSupportedException()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			this.UriInterfaceTypeConverter.ConvertFrom(null, null, null);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertFrom_IfTheValueParameterIsOfATypeThatCanNotBeConvertedFrom_ShouldThrowANotSupportedException()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			this.UriInterfaceTypeConverter.ConvertFrom(null, null, 2);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheValueParameterIsAConcreteUri_ShouldThrowANotSupportedException()
		{
			this.UriInterfaceTypeConverter.ConvertTo(null, null, new Uri("http://localhost"), typeof(string));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheValueParameterIsAString_ShouldThrowANotSupportedException()
		{
			this.UriInterfaceTypeConverter.ConvertTo(null, null, "http://localhost", typeof(string));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheValueParameterIsNotAnUri_ShouldThrowANotSupportedException()
		{
			this.UriInterfaceTypeConverter.ConvertTo(null, null, 2, typeof(string));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheValueParameterIsNullAndTheDestinationTypeCanNotBeConvertedTo_ShouldThrowANotSupportedException()
		{
			Assert.IsNull(this.UriInterfaceTypeConverter.ConvertTo(null, null, null, typeof(IComponent)));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheValueParameterIsNullAndTheDestinationTypeIsString_ShouldThrowANotSupportedException()
		{
			this.UriInterfaceTypeConverter.ConvertTo(null, null, null, typeof(string));
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertTo_IfTheValueParameterIsNullAndTheDestinationTypeIsUri_ShouldThrowANotSupportedException()
		{
			this.UriInterfaceTypeConverter.ConvertTo(null, null, null, typeof(IUri));
		}

		[TestMethod]
		public void IsValid_IfTheValueParameterIsAnUri_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.IsValid(null, new Uri("http://localhost")));
			Assert.IsTrue(this.UriInterfaceTypeConverter.IsValid(null, new Uri("Directory", UriKind.Relative)));
		}

		[TestMethod]
		public void IsValid_IfTheValueParameterIsAnUriInterface_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.IsValid(null, Mock.Of<IUri>()));
		}

		[TestMethod]
		public void IsValid_IfTheValueParameterIsAValidUriString_ShouldReturnTrue()
		{
			Assert.IsTrue(this.UriInterfaceTypeConverter.IsValid(null, "http://localhost"));
			Assert.IsTrue(this.UriInterfaceTypeConverter.IsValid(null, "Directory"));
		}

		[TestMethod]
		public void IsValid_IfTheValueParameterIsNull_ShouldReturnFalse()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			Assert.IsFalse(this.UriInterfaceTypeConverter.IsValid(null, null));
			// ReSharper restore AssignNullToNotNullAttribute
		}

		#endregion
	}
}