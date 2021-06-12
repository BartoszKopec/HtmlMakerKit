using HtmlMakerKit.Core.Element;
using System;

namespace HtmlMakerKit
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DoCore();

			Console.Read();
		}

		private static void DoCore()
		{
			HtmlElement html = HtmlElement.New("html").AddChildren(
				HtmlElement.New("head").AddChildren(
					HtmlElement.New("title").SetInnerHTML("My website!"),
					HtmlElement.New("link").AddAttibutes(
						("rel", "stylesheet"),
						("href", "http://example.org/style.css")
						),
					HtmlElement.New("style").SetInnerHTML(
						"body{color:black;} p{font-size:1.1em;} .title{color:blue;}"
						)
					),
				HtmlElement.New("body").AddChildren(
					HtmlElement.New("h1")
						.AddAttibutes(("class", "title"))
						.SetInnerHTML("Mega welcome on my website!"),
					HtmlElement.New("p").SetInnerHTML("Lorem ipsum dotor amar"),
					HtmlElement.New("script").SetInnerHTML(
						"function job(){console.log(\"bar\");"
						)
					)
				);

			Console.WriteLine(html);
		}
	}
}
