using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests{
	[TestFixture]
	public class QuickTests : FullServerHarness{
		[Expose]
		public string Index(){
			return "Hello World";
		}

		[Test]
		public void ServerStartsWithNoErrors(){}

		[Test]
		public void IndexReturnsHelloWorld(){
			var response = SendRequest("/");
			response.Body.Should().Be("Hello World");
		}
	}
}