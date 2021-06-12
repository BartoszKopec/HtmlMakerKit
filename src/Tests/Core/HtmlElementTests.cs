using HtmlMakerKit.Core;
using HtmlMakerKit.Core.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Core
{
	public class HtmlElementTests
	{
		[Fact]
		public void TestProperties_Initialization()
		{
			HtmlElement htmlElement = new HtmlElement("div");

			Assert.False(string.IsNullOrWhiteSpace(htmlElement.Markup));
			Assert.True(string.IsNullOrWhiteSpace(htmlElement.InnerHTML));
			Assert.NotNull(htmlElement.ChildElements);
			Assert.NotNull(htmlElement.Attributes);
			Assert.Empty(htmlElement.ChildElements);
			Assert.Empty(htmlElement.Attributes);
		}

		[Theory]
		[InlineData("html")]
		[InlineData("p")]
		[InlineData("h1")]
		public void TestProperties_Initialization_With_ValidMarkup(string markup)
		{
			HtmlElement htmlElement = new HtmlElement(markup);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		[InlineData("<>")]
		[InlineData("><")]
		[InlineData(">/<")]
		[InlineData("<  >")]
		[InlineData("</>")]
		[InlineData("<  />")]
		[InlineData("<foo")]
		[InlineData("foo>")]
		[InlineData("foo<")]
		[InlineData(">foo<")]
		[InlineData(">foo")]
		[InlineData("foo<bar")]
		[InlineData("foo>bar")]
		[InlineData("<foo>bar")]
		[InlineData("<foo>")]
		[InlineData("</foo>")]
		[InlineData("<foo/>")]
		public void TestProperties_Initialization_With_WrongMarkup(string markup)
		{
			HtmlElementException exception = Assert.Throws<HtmlElementException>(() => new HtmlElement(markup));
			Assert.False(string.IsNullOrWhiteSpace(exception.Message));
			Assert.False(string.IsNullOrWhiteSpace(HtmlElementException.WRONG_MARKUP));
			Assert.Equal(HtmlElementException.WRONG_MARKUP, exception.Message);
		}

		[Fact]
		public void TestInnerHtml()
		{
			HtmlElement htmlElement = new HtmlElement("div");

			htmlElement.InnerHTML = "foobar";
			Assert.Equal("foobar", htmlElement.InnerHTML);
			htmlElement.InnerHTML = null;
			Assert.Null(htmlElement.InnerHTML);

			htmlElement.SetInnerHTML("foobar");
			Assert.Equal("foobar", htmlElement.InnerHTML);
			htmlElement.SetInnerHTML(null);
			Assert.Null(htmlElement.InnerHTML);
		}

		[Fact]
		public void TestConversionToString()
		{
			string expectedHtml = "<div><p hidden>Hello</p><span class=\"bb-18\">L</span></div>";

			HtmlElement htmlElement = new HtmlElement("div");
			htmlElement.AddChildren(
				HtmlElement.New("p").SetInnerHTML("Hello").AddAttibutes("hidden"),
				HtmlElement.New("span").SetInnerHTML("L").AddAttibutes(("class", "bb-18"))
				);

			string actualHtml = htmlElement.ToString();

			Assert.Equal(expectedHtml, actualHtml);
		}

		[Fact]
		public void TestConversionToString_ByOperator()
		{
			string expectedHtml = "<div><p hidden>Hello</p><span class=\"bb-18\">L</span></div>";

			string actualHtml = HtmlElement.New("div")
				.AddChildren(
					HtmlElement.New("p").SetInnerHTML("Hello").AddAttibutes("hidden"),
					HtmlElement.New("span").SetInnerHTML("L").AddAttibutes(("class", "bb-18"))
				);

			Assert.Equal(expectedHtml, actualHtml);
		}

		[Fact]
		public void TestEquality_WithSuccess()
		{
			HtmlElement element1 = HtmlElement.New("div")
				.AddChildren(
					HtmlElement.New("p").SetInnerHTML("Hello").AddAttibutes("hidden"),
					HtmlElement.New("span").SetInnerHTML("L").AddAttibutes(("class", "bb-18"))
				);
			HtmlElement element2 = HtmlElement.New("div")
				.AddChildren(
					HtmlElement.New("p").SetInnerHTML("Hello").AddAttibutes("hidden"),
					HtmlElement.New("span").SetInnerHTML("L").AddAttibutes(("class", "bb-18"))
				);

			Assert.True(element1.Equals(element2));
		}

		[Fact]
		public void TestEquality_WithFailure()
		{
			HtmlElement element1 = HtmlElement.New("div")
				.AddChildren(
					HtmlElement.New("span").SetInnerHTML("Hello").AddAttibutes("hidden"),
					HtmlElement.New("span").SetInnerHTML("L").AddAttibutes(("class", "bb-18"))
				);
			HtmlElement element2 = HtmlElement.New("div")
				.AddChildren(
					HtmlElement.New("p").SetInnerHTML("Hello").AddAttibutes("hidden"),
					HtmlElement.New("span").SetInnerHTML("L").AddAttibutes(("class", "bb-18"))
				);

			Assert.False(element1.Equals(element2));
		}

	}
}