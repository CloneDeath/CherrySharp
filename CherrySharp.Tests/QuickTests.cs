using System;
using System.Net;
using System.Net.Http;
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
			var client = new HttpClient();
			var response = client.GetAsync(String.Format("http://localhost:{0}/", Quick.Port));

			response.Wait();

			response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
			var body = response.Result.Content.ReadAsStringAsync();
			body.Wait();
			body.Result.Should().Be("Hello World");
		}
	}
}