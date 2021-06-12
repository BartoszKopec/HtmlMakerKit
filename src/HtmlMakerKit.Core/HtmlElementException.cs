using System;

namespace HtmlMakerKit.Core
{
	public class HtmlElementException : Exception
	{
		public const string WRONG_MARKUP = "Format of markup is wrong. Use plain name of markup, in example \"div\" not \"<div>\".";
		public const string EMPTY_ELEMENT = "Given html element is null or empty";
		public const string EMPTY_ATTRIBUTE = "Given html attribute is null or empty";

		public HtmlElementException(string message) : base(message)
		{
		}
	}
}
