using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.IntegrationTests
{
	[TestClass]
	public class UriWrapperTest
	{
		#region Methods

		[TestMethod]
		public void IsDeserializable()
		{
			var uniformResourceIdentifier = "http://localhost/Directory/Sub-Directory/File.txt?Key=Value#Fragment";

			using(var stream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(stream, new UriWrapper(new Uri(uniformResourceIdentifier)));

				stream.Position = 0;

				var uriWrapper = (UriWrapper) new BinaryFormatter().Deserialize(stream);

				Assert.AreEqual(uniformResourceIdentifier, uriWrapper.WrappedInstance.OriginalString);
				Assert.AreEqual("#Fragment", uriWrapper.Fragment);
				Assert.AreEqual("/Directory/Sub-Directory/File.txt", uriWrapper.Path);
				// ReSharper disable PossibleInvalidOperationException
				Assert.AreEqual(80, uriWrapper.Port.Value);
				// ReSharper restore PossibleInvalidOperationException
				Assert.AreEqual("?Key=Value", uriWrapper.Query);
			}

			uniformResourceIdentifier = "Directory/Sub-Directory/File.txt?Key=Value#Fragment";

			using(var stream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(stream, new UriWrapper(new Uri(uniformResourceIdentifier, UriKind.RelativeOrAbsolute)));

				stream.Position = 0;

				var uriWrapper = (UriWrapper) new BinaryFormatter().Deserialize(stream);

				Assert.AreEqual(uniformResourceIdentifier, uriWrapper.WrappedInstance.OriginalString);
				Assert.AreEqual("#Fragment", uriWrapper.Fragment);
				Assert.AreEqual("/Directory/Sub-Directory/File.txt", uriWrapper.Path);
				Assert.IsNull(uriWrapper.Port);
				Assert.AreEqual("?Key=Value", uriWrapper.Query);
			}
		}

		[TestMethod]
		public void IsSerializable()
		{
			using(var stream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(stream, new UriWrapper(new Uri("http://localhost")));
			}
		}

		[TestMethod]
		public void ShouldUseUriInterfaceTypeConverterAsTypeConverter()
		{
			Assert.IsTrue(TypeDescriptor.GetConverter(typeof(UriWrapper)) is UriInterfaceTypeConverter);
		}

		#endregion
	}
}