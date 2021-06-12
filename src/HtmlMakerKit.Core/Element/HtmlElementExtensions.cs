using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlMakerKit.Core.Element
{
	public static class HtmlElementExtensions
	{
		public static HtmlElement SetInnerHTML(this HtmlElement element, string text)
		{
			element.InnerHTML = text;
			return element;
		}
		public static HtmlElement AddChildren(this HtmlElement element, params HtmlElement[] children)
		{
			if (children is null || children.ToList().Exists((e) => e is null))
				throw new HtmlElementException(HtmlElementException.EMPTY_ELEMENT);
			element.ChildElements.AddRange(children);
			return element;
		}
		public static HtmlElement AddAttibutes(this HtmlElement element, params string[] attributes) =>
			element.AddAttibutes(attributes.Select((item) => (item, (string)null)).ToArray());
		public static HtmlElement AddAttibutes(this HtmlElement element, params (string key, string value)[] attributes)
		{
			if (attributes.Select((item)=> item.key).Contains(null))
				throw new HtmlElementException(HtmlElementException.EMPTY_ATTRIBUTE);
			element.AddAttibutes(attributes?.ToDictionary((tuple) => tuple.key, (tuple) => tuple.value));
			return element;
		}
		public static HtmlElement AddAttibutes(this HtmlElement element, Dictionary<string, string> attributes)
		{
			if (attributes is null)
				throw new HtmlElementException(HtmlElementException.EMPTY_ATTRIBUTE);
			foreach ((string key, string value) in attributes)
			{
				element.Attributes.Add(key, value);
			}
			return element;
		}
		public static HtmlElement AddAttibuteOrUpdate(this HtmlElement element, string attribute, string value)
		{
			if (element.HasAttribute(attribute))
			{
				element.Attributes[attribute] = value;
			}
			else
			{
				element.AddAttibutes((attribute, value));
			}

			return element;
		}

		public static bool HasChild(this HtmlElement element, Predicate<HtmlElement> predicate) =>
			HasContent(element) && element.ChildElements.FirstOrDefault((e) => predicate(e)) != null;
		public static bool HasContent(this HtmlElement element) => 
			HasInnerHTML(element) || element.ChildElements.Count > 0;
		public static bool HasInnerHTML(this HtmlElement element) => 
			!string.IsNullOrWhiteSpace(element.InnerHTML);
		public static bool HasAttribute(this HtmlElement element, string attribute) =>
			element.Attributes.ContainsKey(attribute);
	}
}
