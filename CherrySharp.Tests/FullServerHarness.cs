using System;
using System.Diagnostics.Tracing;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests{
	[TestFixture]
	public abstract class FullServerHarness{
		private Exception _startException;

		[SetUp]
		public void SetupServer(){
			Task.Factory.StartNew(() =>{
				try{
					Quick.Start(this);
				}
				catch (Exception ex){
					_startException = ex;
				}
			});
			Thread.Sleep(1000);
			AssertQuickStartHasNoExceptions();
		}

		private void AssertQuickStartHasNoExceptions(){
			if (_startException != null) throw _startException;
		}

		[TearDown]
		public void StopServer(){
			AssertQuickStartHasNoExceptions();
			Quick.Stop();
		}

		protected static Response SendRequest(string request)
		{
			string requestBase = String.Format("http://localhost:{0}/", 8080);

			if (request.StartsWith("/")) request = request.Substring(1);

			CookieContainer cookies = new CookieContainer();

			var handler = new HttpClientHandler{CookieContainer = cookies};

			var client = new HttpClient(handler);

			var fullRequest = new Uri(requestBase + request);
			var response = client.GetAsync(fullRequest);

			bool connected = response.Wait(TimeSpan.FromSeconds(2));
			connected.Should().BeTrue();

			response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
			var body = response.Result.Content.ReadAsStringAsync();
			body.Wait();
			string bodyText = body.Result;
			return new Response(){
				Body = bodyText,
				Cookies = cookies.GetCookies(fullRequest),
			};
		}
	}
}