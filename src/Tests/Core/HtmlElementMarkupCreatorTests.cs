using HtmlMakerKit.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core
{
	public class HtmlElementMarkupCreatorTests
	{

		[Fact]
		public void TestGetAttributesString()
		{
			string expected = "foo f=\"bar\" p=\"1\" hello";

			string actual = HtmlElementMarkupCreator.GetAttributesString(new Dictionary<string, string>
			{
				{ "foo", null },
				{ "f", "bar" },
				{ "p", "1" },
				{ "hello", null },
			});

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestXmlStart_WithAttributes()
		{
			string expected = "<div foo f=\"bar\" p=\"1\">";

			string actual = HtmlElementMarkupCreator.GetMarkupStart("div", HtmlElementMarkupCreator.GetAttributesString(
				new Dictionary<string, string>
				{
					{ "foo", null },
					{ "f", "bar" },
					{ "p", "1" },
				}));

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestXmlStart_WithoutAttributes()
		{
			string expected = "<div>";

			string actual = HtmlElementMarkupCreator.GetMarkupStart("div", "");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestXmlEnd()
		{
			string expected = "</div>";

			string actual = HtmlElementMarkupCreator.GetMarkupEnd("div");

			Assert.Equal(expected, actual);
		}
	}
}
