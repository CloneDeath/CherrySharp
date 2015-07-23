using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests{
	class QuickIndex{
		[Expose]
		public string Index(){
			return "Hello World";
		}
	}

	[TestFixture]
	public class QuickTests{
		[Test]
		public void CanStartServerWithoutCrashing(){
			Task.Factory.StartNew(() =>{
				Thread.Sleep(1000);
				Quick.Stop();
			});
			Quick.Start(new QuickIndex());
		}

		[Test]
		public void IndexReturnsHelloWorld(){
			Task.Factory.StartNew(StartServer);
			Thread.Sleep(1000);

			HttpClient client = new HttpClient();
			var response = client.GetAsync(String.Format("http://localhost:{0}/", Quick.Port));

			response.Wait();

			response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
			var body = response.Result.Content.ReadAsStringAsync();
			body.Wait();
			body.Result.Should().Be("Hello World");

			Quick.Stop();
		}

		private void StartServer(){
			Quick.Start(new QuickIndex());
		}
	}
}