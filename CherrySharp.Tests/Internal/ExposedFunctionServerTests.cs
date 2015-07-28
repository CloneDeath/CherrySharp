using CherrySharp.Tests.Internal.Harnesses;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal{
	[TestFixture]
	public class ExposedFunctionServerTests : ServerTestHarness{
		[Test]
		public void ServerCallsFunctionOfRequestUrl(){
			GetResponseForRequest("/SayHello").Should().Be("Hello");
		}

		[Test]
		public void EmptyRequestReturnsIndex(){
			GetResponseForRequest("/").Should().Be("Index Page");
		}

		[Test]
		public void ServerParsesUrlParameters(){
			GetResponseForRequest("/Echo?input=Hi!").Should().Be("hi!");
		}

		[Test]
		public void DefaultParametersAreUsed(){
			GetResponseForRequest("/Echo").Should().Be("hello");
		}

		[Expose]
		public string Index(){
			return "Index Page";
		}

		[Expose]
		public string SayHello(){
			return "Hello";
		}

		[Expose]
		public string Echo(string input = "HELLO"){
			return input.ToLower();
		}
	}
}