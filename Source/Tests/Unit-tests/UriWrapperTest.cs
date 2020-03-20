using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests
{
	[TestClass]
	public class UriWrapperTest
	{
		#region Methods

		[TestMethod]
		public void GetComponents_Test()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory", UriKind.Relative));

			Assert.AreEqual("/Directory", uriWrapper.GetComponents(UriComponents.AbsoluteUri, UriFormat.Unescaped));
			Assert.AreEqual("/Directory", uriWrapper.GetComponents(UriComponents.AbsoluteUri | UriComponents.Host, UriFormat.Unescaped));
			Assert.IsNull(uriWrapper.GetComponents(UriComponents.Host, UriFormat.Unescaped));
			Assert.IsNull(uriWrapper.GetComponents(UriComponents.Port, UriFormat.Unescaped));
		}

		[TestMethod]
		public void GetLeftPart_IfTheWrappedUriIsRelativeAndThePartParameterIsAuthority_ShouldReturnNull()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory", UriKind.Relative));
			Assert.IsNull(uriWrapper.GetLeftPart(UriPartial.Authority));
		}

		[TestMethod]
		public void GetLeftPart_IfTheWrappedUriIsRelativeAndThePartParameterIsPath_ShouldReturnThePath()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Relative));
			Assert.AreEqual("/Directory/Sub-Directory/File.txt", uriWrapper.GetLeftPart(UriPartial.Path));

			uriWrapper = new UriWrapper(new Uri(string.Empty, UriKind.Relative));
			Assert.AreEqual("/", uriWrapper.GetLeftPart(UriPartial.Path));
		}

		[TestMethod]
		public void GetLeftPart_IfTheWrappedUriIsRelativeAndThePartParameterIsQuery_ShouldReturnThePathAndQuery()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Relative));
			Assert.AreEqual("/Directory/Sub-Directory/File.txt?Key=Value", uriWrapper.GetLeftPart(UriPartial.Query));

			uriWrapper = new UriWrapper(new Uri(string.Empty, UriKind.Relative));
			Assert.AreEqual("/", uriWrapper.GetLeftPart(UriPartial.Query));
		}

		[TestMethod]
		public void GetLeftPart_IfTheWrappedUriIsRelativeAndThePartParameterIsScheme_ShouldReturnNull()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory", UriKind.Relative));
			Assert.IsNull(uriWrapper.GetLeftPart(UriPartial.Scheme));
		}

		[TestMethod]
		public void IsWellFormedOriginalString_IfTheUriIsRelativeAndThePathIsWellformed_ShouldReturnTrue()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory", UriKind.Relative));
			Assert.IsTrue(uriWrapper.IsWellFormedOriginalString());
		}

		[TestMethod]
		public void LocalPath_IfTheWrappedUriIsRelative_ShouldReturnTheRelativePath()
		{
			const string path = "Directory";
			var uriWrapper = new UriWrapper(new Uri(path, UriKind.Relative));
			Assert.AreEqual("/" + path, uriWrapper.LocalPath);
		}

		[TestMethod]
		public void Port_IfTheSchemeIsUnknown_ShouldReturnNull()
		{
			var uriWrapper = new UriWrapper(new Uri("unknown-scheme://localhost", UriKind.Absolute));
			Assert.IsNull(uriWrapper.Port);
		}

		[TestMethod]
		public void Port_IfTheSchemeIsWellknown_ShouldReturnTheDefaultPort()
		{
			var uriWrapper = new UriWrapper(new Uri("http://localhost", UriKind.Absolute));
			// ReSharper disable PossibleInvalidOperationException
			Assert.AreEqual(80, uriWrapper.Port.Value);
			// ReSharper restore PossibleInvalidOperationException
		}

		[TestMethod]
		public void Port_IfTheWrappedUriIsRelative_ShouldReturnNull()
		{
			var uriWrapper = new UriWrapper(new Uri("Directory", UriKind.Relative));
			Assert.IsNull(uriWrapper.Port);
		}

		#endregion
	}
}