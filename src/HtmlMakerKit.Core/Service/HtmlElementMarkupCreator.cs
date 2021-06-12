using System.Collections.Generic;
using System.Text;

namespace HtmlMakerKit.Core.Service
{
	public class HtmlElementMarkupCreator
	{
		public static string GetAttributesString(Dictionary<string, string> attributes)
		{
			StringBuilder attrBuilder = new();
			int i = 0;
			foreach (var attribute in attributes)
			{
				attrBuilder.Append(attribute.Key);
				if (!string.IsNullOrWhiteSpace(attribute.Value))
				{
					attrBuilder.Append($"=\"{attribute.Value}\"");
				}
				if (i < attributes.Count - 1)
				{
					attrBuilder.Append(' ');
				}
				i++;
			}
			return attrBuilder.ToString();
		}

		public static string GetMarkupStart(string markup, string attributes) =>
			string.IsNullOrWhiteSpace(attributes) ? $"<{markup}>" : $"<{markup} {attributes}>";

		public static string GetMarkupEnd(string markup) => $"</{markup}>";
	}
}
