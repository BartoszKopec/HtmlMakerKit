using HtmlMakerKit.Core;
using HtmlMakerKit.Core.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core
{
	public class HtmlElementExtensionsTests
	{
		[Fact]
		public void TestChildrenAdding_WhenNull()
		{
			Assert.False(string.IsNullOrWhiteSpace(HtmlElementException.EMPTY_ELEMENT));

			HtmlElement htmlElement = new HtmlElement("div");
			void AssertIt(HtmlElementException ex)
			{
				Assert.False(string.IsNullOrWhiteSpace(ex.Message));
				Assert.Equal(HtmlElementException.EMPTY_ELEMENT, ex.Message);
			}

			HtmlElementException exception = Assert.Throws<HtmlElementException>(() => htmlElement.AddChildren(null));
			AssertIt(exception);
			exception = Assert.Throws<HtmlElementException>(() => htmlElement.AddChildren(new HtmlElement[] { null }));
			AssertIt(exception);
			exception = Assert.Throws<HtmlElementException>(() => htmlElement.AddChildren(HtmlElement.New("d"), null));
			AssertIt(exception);
		}

		[Fact]
		public void TestChildrenAdding()
		{
			HtmlElement htmlElement = new HtmlElement("div");

			htmlElement.AddChildren(new HtmlElement("p"));
			Assert.Single(htmlElement.ChildElements);
			Assert.Equal("p", htmlElement.ChildElements[0].Markup);

			htmlElement.AddChildren(new HtmlElement("img"), new HtmlElement("a"));
			Assert.Equal(3, htmlElement.ChildElements.Count);
			Assert.Equal("img", htmlElement.ChildElements[1].Markup);
			Assert.Equal("a", htmlElement.ChildElements[2].Markup);
		}

		[Fact]
		public void TestChildConfirmation()
		{
			HtmlElement htmlElement = new HtmlElement("div")
				.AddChildren(HtmlElement.New("p"));

			bool actual = htmlElement.HasChild((e) => e.Markup == "p");

			Assert.True(actual);
		}

		[Fact]
		public void TestChildUnconfirmation()
		{
			HtmlElement htmlElement = new HtmlElement("div")
				.AddChildren(HtmlElement.New("p"));

			bool actual = htmlElement.HasChild((e) => e.Markup == "a");

			Assert.False(actual);
		}

		[Fact]
		public void TestContentConfirmation_With_Children()
		{
			HtmlElement htmlElement = new HtmlElement("div")
				.AddChildren(HtmlElement.New("p"));

			bool actual = htmlElement.HasContent();
			Assert.True(actual);
		}

		[Fact]
		public void TestContentConfirmation_With_InnerHtml()
		{
			HtmlElement htmlElement = new HtmlElement("div");
			htmlElement.InnerHTML = "Foobar";

			bool actual = htmlElement.HasContent();
			Assert.True(actual);
		}

		[Fact]
		public void TestInnerHtmlConfirmation_Filled()
		{
			HtmlElement htmlElement = new HtmlElement("div");
			htmlElement.InnerHTML = "Foobar";

			bool actual = htmlElement.HasInnerHTML();
			Assert.True(actual);
		}

		[Fact]
		public void TestContentUnconfirmation_Empty()
		{
			HtmlElement htmlElement = new HtmlElement("div");

			bool actual = htmlElement.HasInnerHTML();
			Assert.False(actual);
		}

		[Fact]
		public void TestAddAttributes_WithNull()
		{
			Assert.False(string.IsNullOrWhiteSpace(HtmlElementException.EMPTY_ATTRIBUTE));

			HtmlElement htmlElement = new HtmlElement("div");
			void AssertIt(HtmlElementException ex)
			{
				Assert.False(string.IsNullOrWhiteSpace(ex.Message));
				Assert.Equal(HtmlElementException.EMPTY_ATTRIBUTE, ex.Message);
			}

			HtmlElementException exception = Assert.Throws<HtmlElementException>(() => htmlElement.AddAttibutes("p", null));
			AssertIt(exception);
			exception = Assert.Throws<HtmlElementException>(() => htmlElement.AddAttibutes(new (string, string)[]
			{
				( null, null ),
				( null, "value" )
			}));
			AssertIt(exception);
		}

		[Fact]
		public void TestAddAttribute_KeyOnly()
		{
			HtmlElement htmlElement = new HtmlElement("div");

			htmlElement.AddAttibutes("foo");

			Assert.Single(htmlElement.Attributes);
			Assert.Equal("foo", htmlElement.Attributes.ElementAt(0).Key);
			Assert.Null(htmlElement.Attributes.ElementAt(0).Value);
		}

		[Fact]
		public void TestAddAttribute_KeyValue()
		{
			HtmlElement htmlElement = new HtmlElement("div");

			htmlElement.AddAttibutes(("foo", "fofo"));

			Assert.Single(htmlElement.Attributes);
			Assert.Equal("foo", htmlElement.Attributes.ElementAt(0).Key);
			Assert.Equal("fofo", htmlElement.Attributes.ElementAt(0).Value);
		}

		[Fact]
		public void TestAddAttributes_KeysOnly()
		{
			HtmlElement htmlElement = new HtmlElement("div");
			string[] expectedAttributes = new[] { "f", "o", "u" };

			htmlElement.AddAttibutes(expectedAttributes);

			Assert.Equal(3, htmlElement.Attributes.Count);
			Assert.Null(htmlElement.Attributes.ElementAt(0).Value);
			for (int i = 0; i < expectedAttributes.Length; i++)
			{
				Assert.True(htmlElement.Attributes.ContainsKey(expectedAttributes[i]));
			}
		}

		[Fact]
		public void TestAddAttributes_Dictionary()
		{
			HtmlElement htmlElement = new HtmlElement("div");
			Dictionary<string, string> expectedAttributes = new()
			{
				{ "f", "o" },
				{ "o", "a" },
				{ "u", "t" }
			};

			htmlElement.AddAttibutes(expectedAttributes);

			Assert.Equal(3, htmlElement.Attributes.Count);
			Assert.Equal(expectedAttributes.ElementAt(0).Value, htmlElement.Attributes.ElementAt(0).Value);
			for (int i = 0; i < expectedAttributes.Count; i++)
			{
				Assert.True(htmlElement.Attributes.ContainsKey(expectedAttributes.ElementAt(i).Key));
			}
		}

		[Fact]
		public void TestAddAttributes_KeysValues()
		{
			HtmlElement htmlElement = new HtmlElement("div");
			(string, string)[] expectedAttributes = new[]
			{
				( "f", "o" ),
				( "o", "a" ),
				( "u", "t" )
			};

			htmlElement.AddAttibutes(expectedAttributes);

			Assert.Equal(3, htmlElement.Attributes.Count);
			Assert.Equal(expectedAttributes.ElementAt(0).Item2, htmlElement.Attributes.ElementAt(0).Value);
			for (int i = 0; i < expectedAttributes.Length; i++)
			{
				Assert.True(htmlElement.Attributes.ContainsKey(expectedAttributes[i].Item1));
			}
		}

		[Fact]
		public void TestAddOrUpdateAttribute()
		{
			const string attribute = "style";
			static void Assertion(Dictionary<string, string> attributes, string value)
			{
				Assert.Single(attributes);
				Assert.Collection(attributes,
					(only) =>
					{
						Assert.True(only.Key == attribute);
						Assert.True(only.Value == value);
					});
			}
			HtmlElement htmlElement = HtmlElement.New("div");

			htmlElement.AddAttibuteOrUpdate(attribute, "cool");
			Assertion(htmlElement.Attributes, "cool");

			htmlElement.AddAttibuteOrUpdate(attribute, "display:none;");
			Assertion(htmlElement.Attributes, "display:none;");
		}

		[Fact]
		public void TestAttributeConfirmation()
		{
			HtmlElement htmlElement = new HtmlElement("div")
				.AddAttibutes("foo");

			bool actual = htmlElement.HasAttribute("foo");

			Assert.True(actual);
		}

		[Fact]
		public void TestAttributeUnconfirmation()
		{
			HtmlElement htmlElement = new HtmlElement("div")
				.AddAttibutes("foo");

			bool actual = htmlElement.HasAttribute("bar");

			Assert.False(actual);
		}


	}
}
