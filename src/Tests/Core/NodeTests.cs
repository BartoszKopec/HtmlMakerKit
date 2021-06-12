using HtmlMakerKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core
{
	public class NodeTests
	{
		[Theory]
		[InlineData(null, 0, null, null)]
		[InlineData("foo", 0, null, null)]
		[InlineData("foo", 5, null, null)]
		[InlineData("foo", 5, "hello", null)]
		[InlineData("foo", 5, "hello", "world")]
		public void TestFieldsInitializatiton(string name, int depth, string content, string attributes)
		{
			Node node = new(name, depth, content, attributes);

			Assert.Equal(name, node.Name);
			Assert.Equal(depth, node.Depth);
			Assert.Equal(content, node.Content);
			Assert.Equal(attributes, node.Attributes);
		}
	}
}
