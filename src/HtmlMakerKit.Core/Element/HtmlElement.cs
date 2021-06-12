using HtmlMakerKit.Core.Architecture;
using HtmlMakerKit.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlMakerKit.Core.Element
{
	public class HtmlElement
	{
		public static implicit operator string(HtmlElement element) => element.ToString();
		public static HtmlElement New(string markup) => new(markup);

		private readonly Regex _markupRegex = new(Const.REGEX_MARKUP);

		public HtmlElement(string markup)
		{
			if (string.IsNullOrWhiteSpace(markup) || !_markupRegex.IsMatch(markup))
				throw new HtmlElementException(HtmlElementException.WRONG_MARKUP);
			Attributes = new Dictionary<string, string>();
			ChildElements = new List<HtmlElement>();
			Markup = markup;
			InnerHTML = string.Empty;
		}

		public Dictionary<string, string> Attributes { get; init; }
		public List<HtmlElement> ChildElements { get; init; }
		public string Markup { get; init; }
		public string InnerHTML { get; set; }

		public override string ToString() => HtmlStringCreator.GetString(this);
		public override bool Equals(object obj)
		{
			if (obj is null || obj.GetType() != typeof(HtmlElement))
				return false;

			HtmlElement elementToCheck = obj as HtmlElement;
			return this.ToString() == elementToCheck.ToString();
		}
	}
}