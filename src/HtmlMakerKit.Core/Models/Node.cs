namespace HtmlMakerKit.Core.Models
{
	public record Node
	{
		public Node(string name, int depth, string content, string attributes)
		{
			Name = name;
			Depth = depth;
			Content = content;
			Attributes = attributes;
		}

		public string Name { get; init; }
		public int Depth { get; init; }
		public string Content { get; init; }
		public string Attributes { get; init; }
	}
}
