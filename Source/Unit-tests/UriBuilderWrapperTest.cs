using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.UnitTests
{
	[TestClass]
	public class UriBuilderWrapperTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithStringParameter_IfTheUniformResourceIdentifierContainsQueryAndFragment_ShouldCreateAnUriBuilderWrapperThatReturnsAnUriWithoutDoubleQuestionMarksAndWithoutDoubleHashSigns()
		{
			const string uniformResourceIdentifier = "http://localhost/?Key=Value#Test";
			Assert.AreEqual(uniformResourceIdentifier, new UriBuilderWrapper(uniformResourceIdentifier).Uri.ToString());
		}

		[TestMethod]
		public void Host_Set_IfTheValueParameterIsNotNull_ShouldMakeItAbsolute()
		{
			var uriWrapper = new UriBuilderWrapper();

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.Host = null;

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.Host = string.Empty;

			Assert.IsTrue(uriWrapper.IsAbsolute);
			Assert.IsTrue(uriWrapper.Uri.IsAbsolute);
		}

		[TestMethod]
		public void IsDotNetFrameworkContext_ShouldReturnFalse()
		{
			Assert.IsFalse(new UriBuilderWrapper().IsDotNetFrameworkContext);
		}

		[TestMethod]
		public void Password_Set_IfTheValueParameterIsNotNull_ShouldMakeItAbsolute()
		{
			var uriWrapper = new UriBuilderWrapper();

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.Password = null;

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.Password = string.Empty;

			Assert.IsTrue(uriWrapper.IsAbsolute);
			Assert.IsTrue(uriWrapper.Uri.IsAbsolute);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Port_Set_IfTheValueParameterIsGreaterThanTheMaxiumumValueOfAnUnsigned16BitInteger_ShouldThrowAnArgumentOutOfRangeException()
		{
			var greaterThanTheMaxiumumValueOfAnUnsigned16BitInteger = new Random().Next(ushort.MaxValue + 1, int.MaxValue);

			try
			{
				new UriBuilderWrapper().Port = greaterThanTheMaxiumumValueOfAnUnsigned16BitInteger;
			}
			catch(ArgumentOutOfRangeException argumentOutOfRangeException)
			{
				if(string.Equals(argumentOutOfRangeException.ParamName, "value", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Port_Set_IfTheValueParameterIsLessThanMinusOne_ShouldThrowAnArgumentOutOfRangeException()
		{
			var lessThanMinusOne = new Random().Next(1, int.MaxValue) * -1;

			try
			{
				new UriBuilderWrapper().Port = lessThanMinusOne;
			}
			catch(ArgumentOutOfRangeException argumentOutOfRangeException)
			{
				if(string.Equals(argumentOutOfRangeException.ParamName, "value", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void Port_Set_IfTheValueParameterIsNotNull_ShouldMakeItAbsolute()
		{
			var uriWrapper = new UriBuilderWrapper();

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			foreach(var number in new[] {-1, 0, 1, 5})
			{
				uriWrapper.Port = null;

				Assert.IsFalse(uriWrapper.IsAbsolute);
				Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

				uriWrapper.Port = number;

				Assert.IsTrue(uriWrapper.IsAbsolute);
				Assert.IsTrue(uriWrapper.Uri.IsAbsolute);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Scheme_Set_IfTheValueIsInvalid_ShouldThrowAnArgumentException()
		{
			try
			{
				new UriBuilderWrapper().Scheme = " ";
			}
			catch(ArgumentException argumentException)
			{
				if(string.Equals(argumentException.ParamName, "value", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void Scheme_Set_IfTheValueParameterIsNotNull_ShouldMakeItAbsolute()
		{
			var uriWrapper = new UriBuilderWrapper();

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.Scheme = null;

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.Scheme = string.Empty;

			Assert.IsTrue(uriWrapper.IsAbsolute);
			Assert.IsTrue(uriWrapper.Uri.IsAbsolute);
		}

		[TestMethod]
		public void Uri_Get_IfConstructedWithAnInitialUriAndTheFragmentHasBeenSet_ShouldReturnAnUriWithTheFragment()
		{
			var uriBuilderWrapper = new UriBuilderWrapper("http://localhost/")
			{
				Fragment = "#Test"
			};

			Assert.AreEqual("http://localhost/#Test", uriBuilderWrapper.Uri.ToString());
		}

		[TestMethod]
		public void Uri_Get_IfConstructedWithAnInitialUriAndTheQueryHasBeenSet_ShouldReturnAnUriWithTheQuery()
		{
			var uriBuilderWrapper = new UriBuilderWrapper("http://localhost/")
			{
				Query = "Key=Value"
			};

			Assert.AreEqual("http://localhost/?Key=Value", uriBuilderWrapper.Uri.ToString());
		}

		[TestMethod]
		public void Uri_Get_IfTheFragmentHasBeenSetWithALeadingHashSign_ShouldReturnAnUriWithoutDoubleLeadingHashSigns()
		{
			var uriBuilderWrapper = new UriBuilderWrapper
			{
				Fragment = "#Test"
			};

			Assert.AreEqual("/#Test", uriBuilderWrapper.Uri.ToString());
		}

		[TestMethod]
		public void Uri_Get_IfTheQueryHasBeenSetWithALeadingQuestionMark_ShouldReturnAnUriWithoutDoubleLeadingQuestionMarks()
		{
			var uriBuilderWrapper = new UriBuilderWrapper
			{
				Query = "?Key=Value"
			};

			Assert.AreEqual("/?Key=Value", uriBuilderWrapper.Uri.ToString());
		}

		[TestMethod]
		public void Uri_ShouldReturnARelativeUriByDefault()
		{
			var uri = new UriBuilderWrapper().Uri;

			Assert.IsFalse(uri.IsAbsolute);
			Assert.AreEqual("/", uri.OriginalString);
		}

		[TestMethod]
		public void UserName_Set_IfTheValueParameterIsNotNull_ShouldMakeItAbsolute()
		{
			var uriWrapper = new UriBuilderWrapper();

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.UserName = null;

			Assert.IsFalse(uriWrapper.IsAbsolute);
			Assert.IsFalse(uriWrapper.Uri.IsAbsolute);

			uriWrapper.UserName = string.Empty;

			Assert.IsTrue(uriWrapper.IsAbsolute);
			Assert.IsTrue(uriWrapper.Uri.IsAbsolute);
		}

		#endregion
	}
}