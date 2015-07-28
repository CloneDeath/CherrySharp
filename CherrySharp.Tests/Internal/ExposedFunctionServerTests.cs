using CherrySharp.Internal;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal{
	[TestFixture]
	public class ExposedFunctionServerTests{
		private ExposedFunctionServer _server;

		[SetUp]
		public void SetupServer(){
			_server = new ExposedFunctionServer(this);
		}

		[Test]
		public void ServerCallsFunctionOfRequestUrl(){
			_server.GetResponseForRequest("/SayHello").Should().Be("Hello");
		}

		[Test]
		public void EmptyRequestReturnsIndex(){
			_server.GetResponseForRequest("/").Should().Be("Index Page");
		}

		[Test]
		public void ServerParsesUrlParameters()
		{
			_server.GetResponseForRequest("/Echo?input=Hi!").Should().Be("hi!");
		}

		[Test]
		public void DefaultParametersAreUsed(){
			_server.GetResponseForRequest("/Echo").Should().Be("hello");
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