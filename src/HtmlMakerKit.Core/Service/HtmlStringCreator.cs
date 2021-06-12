using HtmlMakerKit.Core.Element;
using HtmlMakerKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlMakerKit.Core.Service
{
	public class HtmlStringCreator
	{
		public static string GetString(HtmlElement element)
		{
			StringBuilder builder = new();
			builder.Append(HtmlElementMarkupCreator.GetMarkupStart(
				element.Markup,
				HtmlElementMarkupCreator.GetAttributesString(element.Attributes)));

			if (element.HasContent())
			{
				if (element.HasInnerHTML())
				{
					builder.Append(element.InnerHTML);
				}
				else
				{
					List<Node> nodes = new();
					foreach (var markup in element.ChildElements)
					{
						Stack<(HtmlElement, int depth)> stack = new();
						stack.Push((markup, 1));
						while (stack.Count > 0)
						{
							(HtmlElement current, int depth) = stack.Pop();
							if (current.HasContent())
							{
								if (current.HasInnerHTML())
								{
									nodes.Add(new Node(
										current.Markup,
										depth,
										current.InnerHTML,
										HtmlElementMarkupCreator.GetAttributesString(current.Attributes)));
								}
								else
								{
									nodes.Add(new Node(
										current.Markup,
										depth,
										"",
										HtmlElementMarkupCreator.GetAttributesString(current.Attributes)));
									IEnumerable<HtmlElement> innerChildren = current.ChildElements.AsEnumerable().Reverse();
									foreach (HtmlElement child in innerChildren)
									{
										int childDepth = depth + 1;
										stack.Push((child, childDepth));
									}
								}
							}
							else
							{
								nodes.Add(new Node(
									current.Markup,
									depth,
									"",
									HtmlElementMarkupCreator.GetAttributesString(current.Attributes)));
							}
						}
					}

					for (int i = 0; i < nodes.Count; i++)
					{
						Node node = nodes[i];
						builder.Append(HtmlElementMarkupCreator.GetMarkupStart(node.Name, node.Attributes));
						if (!string.IsNullOrWhiteSpace(node.Content))
						{
							builder.Append(node.Content);
						}
						if (i + 1 == nodes.Count || nodes[i + 1].Depth <= node.Depth)
						{
							builder.Append(HtmlElementMarkupCreator.GetMarkupEnd(node.Name));
						}
						if (i + 1 < nodes.Count && nodes[i + 1].Depth < node.Depth)
						{
							for (int j = i; j >= 0; j--)
							{
								Node previousNode = nodes[j];
								if (previousNode.Depth < node.Depth)
								{
									builder.Append(HtmlElementMarkupCreator.GetMarkupEnd(previousNode.Name));
									break;
								}
							}
						}
					}
					int currentDepth = nodes[^1].Depth;
					for (int j = nodes.Count - 2; j >= 0; j--)
					{
						Node previousNode = nodes[j];
						if (previousNode.Depth < currentDepth)
						{
							currentDepth--;
							builder.Append(HtmlElementMarkupCreator.GetMarkupEnd(previousNode.Name));
						}
					}
				}
			}
			builder.Append(HtmlElementMarkupCreator.GetMarkupEnd(element.Markup));
			return builder.ToString();
		}
	}
}
