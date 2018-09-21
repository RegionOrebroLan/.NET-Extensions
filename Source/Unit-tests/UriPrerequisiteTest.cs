using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests
{
	[TestClass]
	public class UriPrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void AbsolutePath_IfTheUriIsAbsolute_ShouldReturnThePath()
		{
			var absoluteUri = new Uri("http://localhost/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Absolute);
			Assert.AreEqual("/Directory/Sub-Directory/File.txt", absoluteUri.AbsolutePath);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void AbsolutePath_IfTheUriIsRelative_ShouldThrowAnInvalidOperationException()
		{
			var relativeUri = new Uri("/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Relative);
			// ReSharper disable UnusedVariable
			var absolutePath = relativeUri.AbsolutePath;
			// ReSharper restore UnusedVariable
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetComponents_IfTheUriIsRelative_ShouldThrowAnInvalidOperationException()
		{
			var relativeUri = new Uri("/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Relative);
			// ReSharper disable UnusedVariable
			var components = relativeUri.GetComponents(UriComponents.AbsoluteUri, UriFormat.Unescaped);
			// ReSharper restore UnusedVariable
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetLeftPart_IfTheUriIsRelative_ShouldThrowAnInvalidOperationException()
		{
			var relativeUri = new Uri("/Directory/Sub-Directory/File.txt", UriKind.Relative);
			// ReSharper disable UnusedVariable
			var leftPart = relativeUri.GetLeftPart(UriPartial.Path);
			// ReSharper restore UnusedVariable
		}

		[TestMethod]
		public void GetLeftPart_Test()
		{
			var uri = new Uri("http://localhost/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Absolute);

			Assert.AreEqual("http://localhost", uri.GetLeftPart(UriPartial.Authority));
			Assert.AreEqual("http://localhost/Directory/Sub-Directory/File.txt", uri.GetLeftPart(UriPartial.Path));
			Assert.AreEqual("http://localhost/Directory/Sub-Directory/File.txt?Key=Value", uri.GetLeftPart(UriPartial.Query));
			Assert.AreEqual("http://", uri.GetLeftPart(UriPartial.Scheme));
		}

		[TestMethod]
		public void IsAbsoluteUri_IfTheUriIsRelative_ShouldNotThrowAnException()
		{
			const string uniformResourceIdentifier = "/Directory/Sub-Directory/File.txt";
			var relativeUri = new Uri(uniformResourceIdentifier, UriKind.Relative);
			Assert.IsFalse(relativeUri.IsAbsoluteUri);
		}

		[TestMethod]
		public void IsBaseOf_IfTheUriIsRelative_ShouldNotThrowAnException()
		{
			const string path = "/Directory/Sub-Directory/File.txt";
			var relativeUri = new Uri(path, UriKind.Relative);
			Assert.IsFalse(relativeUri.IsBaseOf(new Uri("http://localhost" + path, UriKind.Absolute)));
		}

		[TestMethod]
		public void IsBaseOf_IfTheUriParamterIsRelative_ShouldNotThrowAnException()
		{
			const string path = "/Directory/Sub-Directory/File.txt";
			var absoluteUri = new Uri("http://localhost" + path, UriKind.Absolute);
			Assert.IsTrue(absoluteUri.IsBaseOf(new Uri(path, UriKind.Relative)));
		}

		[TestMethod]
		public void IsWellFormedOriginalString_IfTheUriIsRelative_ShouldNotThrowAnException()
		{
			var relativeUri = new Uri("/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Relative);
			Assert.IsFalse(relativeUri.IsWellFormedOriginalString());
		}

		[TestMethod]
		public void LocalPath_IfTheUriIsAnAbsoluteHttpUri_ShouldBeEqualToTheAbsolutePath()
		{
			this.LocalPathTest();
		}

		[TestMethod]
		public void LocalPath_IfTheUriIsAnAbsoluteHttpUri_ShouldReturnThePath()
		{
			this.LocalPathTest();
		}

		protected internal virtual void LocalPathTest()
		{
			var path = "/Directory";
			var uniformResourceIdentifier = "http://localhost" + path;
			var uri = new Uri(uniformResourceIdentifier, UriKind.Absolute);
			Assert.AreEqual(path, uri.LocalPath);
			Assert.AreEqual(uri.AbsolutePath, uri.LocalPath);

			path = "/Directory/";
			uniformResourceIdentifier = "http://localhost" + path;
			uri = new Uri(uniformResourceIdentifier, UriKind.Absolute);
			Assert.AreEqual(path, uri.LocalPath);
			Assert.AreEqual(uri.AbsolutePath, uri.LocalPath);

			path = "/Directory/Sub-Directory/File.txt";
			uniformResourceIdentifier = "http://localhost" + path + "?Key=Value#Fragment";
			uri = new Uri(uniformResourceIdentifier, UriKind.Absolute);
			Assert.AreEqual(path, uri.LocalPath);
			Assert.AreEqual(uri.AbsolutePath, uri.LocalPath);
		}

		[TestMethod]
		public void OriginalString_IfTheUriIsRelative_ShouldNotThrowAnException()
		{
			const string uniformResourceIdentifier = "Directory";
			var relativeUri = new Uri(uniformResourceIdentifier, UriKind.Relative);
			Assert.AreEqual(uniformResourceIdentifier, relativeUri.OriginalString);
		}

		[TestMethod]
		public void ToString_IfTheUriContainsAFragment_ShouldReturnAStringContainingTheFragment()
		{
			const string relativePart = "/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment";

			var absoluteUri = new Uri("http://localhost" + relativePart, UriKind.Absolute);
			Assert.AreEqual("http://localhost" + relativePart, absoluteUri.ToString());

			var relativeUri = new Uri(relativePart, UriKind.Relative);
			Assert.AreEqual(relativePart, relativeUri.ToString());
		}

		[TestMethod]
		public void UserEscaped_IfTheUriIsRelative_ShouldNotThrowAnException()
		{
			const string uniformResourceIdentifier = "/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment";
			var relativeUri = new Uri(uniformResourceIdentifier, UriKind.Relative);
			Assert.IsFalse(relativeUri.UserEscaped);
		}

		#endregion
	}
}