using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests{
	[TestFixture]
	public class ClientSessionCookieTests : FullServerHarness{
		[Expose]
		public string Index(){
			return "Hello World";
		}

		[Test]
		public void ResponseGivesCookie(){
			CookieContainer cookies = new CookieContainer();

			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;

			var client = new HttpClient(handler);
			var requestUri = new Uri(String.Format("http://localhost:{0}/", 8080));
			var response = client.GetAsync(requestUri);

			response.Wait();
			response.Result.StatusCode.Should().Be(HttpStatusCode.OK);

			cookies.GetCookies(requestUri).Count.Should().BeGreaterThan(0);
		}
	}
}