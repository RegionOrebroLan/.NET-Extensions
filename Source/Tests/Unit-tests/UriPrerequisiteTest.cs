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

			var _ = relativeUri.AbsolutePath;
		}

		[TestMethod]
		[ExpectedException(typeof(UriFormatException))]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsABackSlashAndTheUriKindParameterIsAbsolute_ShouldThrowAnUriFormatException()
		{
			var _ = new Uri(@"\", UriKind.Absolute);
		}

		[TestMethod]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsABackSlashAndTheUriKindParameterIsRelative_ShouldResultInARelativeUriWithTheStringRepresentaionOfABackSlash()
		{
			var uri = new Uri(@"\", UriKind.Relative);
			Assert.IsFalse(uri.IsAbsoluteUri);
			Assert.AreEqual(@"\", uri.OriginalString);
			Assert.AreEqual(@"\", uri.ToString());
		}

		[TestMethod]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsABackSlashAndTheUriKindParameterIsRelativeOrAbsolute_ShouldResultInARelativeUriWithTheStringRepresentaionOfABackSlash()
		{
			var uri = new Uri(@"\", UriKind.RelativeOrAbsolute);
			Assert.IsFalse(uri.IsAbsoluteUri);
			Assert.AreEqual(@"\", uri.OriginalString);
			Assert.AreEqual(@"\", uri.ToString());
		}

		[TestMethod]
		[ExpectedException(typeof(UriFormatException))]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsAnEmptyStringAndTheUriKindParameterIsAbsolute_ShouldThrowAnUriFormatException()
		{
			var _ = new Uri(string.Empty, UriKind.Absolute);
		}

		[TestMethod]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsAnEmptyStringAndTheUriKindParameterIsRelative_ShouldResultInARelativeUriWithTheStringRepresentaionOfAnEmptyString()
		{
			var uri = new Uri(string.Empty, UriKind.Relative);
			Assert.IsFalse(uri.IsAbsoluteUri);
			Assert.AreEqual(string.Empty, uri.OriginalString);
			Assert.AreEqual(string.Empty, uri.ToString());
		}

		[TestMethod]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsAnEmptyStringAndTheUriKindParameterIsRelativeOrAbsolute_ShouldResultInARelativeUriWithTheStringRepresentaionOfAnEmptyString()
		{
			var uri = new Uri(string.Empty, UriKind.RelativeOrAbsolute);
			Assert.IsFalse(uri.IsAbsoluteUri);
			Assert.AreEqual(string.Empty, uri.OriginalString);
			Assert.AreEqual(string.Empty, uri.ToString());
		}

		[TestMethod]
		[ExpectedException(typeof(UriFormatException))]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsASlashAndTheUriKindParameterIsAbsolute_ShouldThrowAnUriFormatException()
		{
			var _ = new Uri("/", UriKind.Absolute);
		}

		[TestMethod]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsASlashAndTheUriKindParameterIsRelative_ShouldResultInARelativeUriWithTheStringRepresentaionOfASlash()
		{
			var uri = new Uri("/", UriKind.Relative);
			Assert.IsFalse(uri.IsAbsoluteUri);
			Assert.AreEqual("/", uri.OriginalString);
			Assert.AreEqual("/", uri.ToString());
		}

		[TestMethod]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsASlashAndTheUriKindParameterIsRelativeOrAbsolute_ShouldResultInARelativeUriWithTheStringRepresentaionOfASlash()
		{
			var uri = new Uri("/", UriKind.RelativeOrAbsolute);
			Assert.IsFalse(uri.IsAbsoluteUri);
			Assert.AreEqual("/", uri.OriginalString);
			Assert.AreEqual("/", uri.ToString());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsNullAndTheUriKindParameterIsAbsolute_ShouldThrowAnArgumentNullException()
		{
			var _ = new Uri(null, UriKind.Absolute);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsNullAndTheUriKindParameterIsRelative_ShouldThrowAnArgumentNullException()
		{
			var _ = new Uri(null, UriKind.Relative);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_WithStringAndUriKindParameters_IfTheStringParameterIsNullAndTheUriKindParameterIsRelativeOrAbsolute_ShouldThrowAnArgumentNullException()
		{
			var _ = new Uri(null, UriKind.RelativeOrAbsolute);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetComponents_IfTheUriIsRelative_ShouldThrowAnInvalidOperationException()
		{
			var relativeUri = new Uri("/Directory/Sub-Directory/File.txt?Key=Value#Our-fragment", UriKind.Relative);

			var _ = relativeUri.GetComponents(UriComponents.AbsoluteUri, UriFormat.Unescaped);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetLeftPart_IfTheUriIsRelative_ShouldThrowAnInvalidOperationException()
		{
			var relativeUri = new Uri("/Directory/Sub-Directory/File.txt", UriKind.Relative);

			var _ = relativeUri.GetLeftPart(UriPartial.Path);
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