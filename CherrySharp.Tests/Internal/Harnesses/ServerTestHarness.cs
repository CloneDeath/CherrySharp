using CherrySharp.Internal;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal.Harnesses{
	[TestFixture]
	public abstract class ServerTestHarness{
		private ExposedFunctionServer _server;

		[SetUp]
		public void SetupServer(){
			_server = new ExposedFunctionServer(this);
		}

		protected string GetResponseForRequest(string request){
			return GetResponseForRequest(request, "");
		}

		protected string GetResponseForRequest(string request, string body){
			return _server.GetResponseForRequest(new Request(null, request, body));
		}
	}
}