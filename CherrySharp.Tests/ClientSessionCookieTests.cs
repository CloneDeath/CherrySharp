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
			var response = SendRequest("/");

			response.Cookies.Count.Should().BeGreaterThan(0);
		}
	}
}