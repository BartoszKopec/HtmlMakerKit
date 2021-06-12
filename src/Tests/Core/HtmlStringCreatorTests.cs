using HtmlMakerKit.Core.Element;
using HtmlMakerKit.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core
{
	public class HtmlStringCreatorTests
	{
		[Fact]
		public void GetString_With_ValidModel()
		{
			HtmlElement element = HtmlElement.New("html").AddChildren(
				HtmlElement.New("head").AddChildren(
					HtmlElement.New("title").SetInnerHTML("Foobar")
					),
				HtmlElement.New("body").AddChildren(
					HtmlElement.New("p").AddAttibutes("hidden"),
					HtmlElement.New("p").AddAttibutes("hidden")
						.SetInnerHTML("BarBar")
					)
				);
			string expectedString = "<html><head><title>Foobar</title></head><body><p hidden></p><p hidden>BarBar</p></body></html>";

			string actualString = HtmlStringCreator.GetString(element);

			Assert.Equal(expectedString, actualString);
		}
	}
}
