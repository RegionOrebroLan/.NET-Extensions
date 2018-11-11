using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests
{
	[TestClass]
	public class UriBuilderPrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithNoParameter_ShouldResultInAnUriBuilderWithADefaultUri()
		{
			var uriBuilder = new UriBuilder();
			Assert.AreEqual("http://localhost/", uriBuilder.Uri.OriginalString);
			Assert.AreEqual("http://localhost/", uriBuilder.Uri.ToString());
		}

		[TestMethod]
		public void Constructor_WithSchemeAndHostParameters_IfTheHostParameterIsAnEmptyString_ShouldResultInAnUriBuilderWhereHostIsAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder("scheme", string.Empty).Host);
		}

		[TestMethod]
		public void Constructor_WithSchemeAndHostParameters_IfTheHostParameterIsNull_ShouldResultInAnUriBuilderWhereHostIsAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder("scheme", null).Host);
		}

		[TestMethod]
		public void Constructor_WithSchemeAndHostParameters_IfTheSchemeParameterIsAnEmptyString_ShouldResultInAnUriBuilderWhereSchemeIsAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder(string.Empty, "host").Scheme);
		}

		[TestMethod]
		public void Constructor_WithSchemeAndHostParameters_IfTheSchemeParameterIsNull_ShouldResultInAnUriBuilderWhereSchemeIsAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder(null, "host").Scheme);
		}

		[TestMethod]
		public void Constructor_WithSchemeAndHostParameters_ShouldResultInAnUriBuilderWithAnUriWhereOriginalStringEndsWithASlash()
		{
			var uriBuilder = new UriBuilder("scheme", "host");
			Assert.IsTrue(uriBuilder.Uri.OriginalString.EndsWith("/", StringComparison.Ordinal));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		[SuppressMessage("Usage", "CA1806:Do not ignore method results")]
		public void Constructor_WithUriParameter_IfTheUriParameterIsARelativeUri_ShouldThrowAnInvalidOperationException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new UriBuilder(new Uri("/", UriKind.Relative));
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		public void Host_Get_IfTheHostIsSetToAnEmptyString_ShouldReturnAWhiteSpace()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {Host = string.Empty}.Host);
		}

		[TestMethod]
		public void Host_Get_IfTheHostIsSetToAWhiteSpace_ShouldReturnAWhiteSpace()
		{
			Assert.AreEqual(" ", new UriBuilder {Host = " "}.Host);
		}

		[TestMethod]
		public void Host_Get_IfTheHostIsSetToNull_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {Host = null}.Host);
		}

		[TestMethod]
		public void Host_Get_ShouldReturnLocalhostByDefault()
		{
			Assert.AreEqual("localhost", new UriBuilder().Host);
		}

		[TestMethod]
		public void Password_Get_IfThePasswordIsSetToAnEmptyString_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {Password = string.Empty}.Password);
		}

		[TestMethod]
		public void Password_Get_IfThePasswordIsSetToAWhiteSpace_ShouldReturnAWhiteSpace()
		{
			Assert.AreEqual(" ", new UriBuilder {Password = " "}.Password);
		}

		[TestMethod]
		public void Password_Get_IfThePasswordIsSetToNull_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {Password = null}.Password);
		}

		[TestMethod]
		public void Password_Get_ShouldReturnAnEmptyStringByDefault()
		{
			Assert.AreEqual(string.Empty, new UriBuilder().Password);
		}

		[TestMethod]
		public void Scheme_Get_IfTheSchemeIsSetToAnEmptyString_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {Scheme = string.Empty}.Scheme);
		}

		[TestMethod]
		public void Scheme_Get_IfTheSchemeIsSetToNull_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {Scheme = null}.Scheme);
		}

		[TestMethod]
		public void Scheme_Get_ShouldReturnHttpByDefault()
		{
			Assert.AreEqual("http", new UriBuilder().Scheme);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Usage", "CA1806:Do not ignore method results")]
		public void Scheme_Set_IfTheValueIsAWhiteSpace_ShouldThrowAnArgumentException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new UriBuilder {Scheme = " "};
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		public void Uri_IfThePasswordIsSetToAnEmptyString_ShouldReturnAnUriWithoutThePassword()
		{
			Assert.AreEqual("http://localhost/", new UriBuilder {Password = string.Empty}.Uri.OriginalString);
		}

		[TestMethod]
		[ExpectedException(typeof(UriFormatException))]
		public void Uri_IfThePasswordIsSetToAWhiteSpaceWithoutSettingTheUserName_ShouldThrowAnUriFormatException()
		{
			// ReSharper disable UnusedVariable
			var uri = new UriBuilder {Password = " "}.Uri;
			// ReSharper restore UnusedVariable
		}

		[TestMethod]
		public void Uri_IfThePasswordIsSetToNull_ShouldReturnAnUriWithoutThePassword()
		{
			Assert.AreEqual("http://localhost/", new UriBuilder {Password = null}.Uri.OriginalString);
		}

		[TestMethod]
		public void Uri_IfTheUserNameIsSetToAnEmptyString_ShouldReturnAnUriWithoutTheUserName()
		{
			Assert.AreEqual("http://localhost/", new UriBuilder {UserName = string.Empty}.Uri.OriginalString);
		}

		[TestMethod]
		public void Uri_IfTheUserNameIsSetToAWhiteSpace_ShouldReturnAnUriWithTheUserNameIncluded()
		{
			Assert.AreEqual("http:// @localhost/", new UriBuilder {UserName = " "}.Uri.OriginalString);
		}

		[TestMethod]
		public void Uri_IfTheUserNameIsSetToNull_ShouldReturnAnUriWithoutTheUserName()
		{
			Assert.AreEqual("http://localhost/", new UriBuilder {UserName = null}.Uri.OriginalString);
		}

		[TestMethod]
		public void UserName_Get_IfTheUserNameIsSetToAnEmptyString_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {UserName = string.Empty}.UserName);
		}

		[TestMethod]
		public void UserName_Get_IfTheUserNameIsSetToAWhiteSpace_ShouldReturnAWhiteSpace()
		{
			Assert.AreEqual(" ", new UriBuilder {UserName = " "}.UserName);
		}

		[TestMethod]
		public void UserName_Get_IfTheUserNameIsSetToNull_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new UriBuilder {UserName = null}.UserName);
		}

		[TestMethod]
		public void UserName_Get_ShouldReturnAnEmptyStringByDefault()
		{
			Assert.AreEqual(string.Empty, new UriBuilder().UserName);
		}

		#endregion
	}
}