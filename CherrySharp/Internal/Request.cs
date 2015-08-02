using CherrySharp.Internal.Sessions;
using CherrySharp.Internal.UriParsing;

namespace CherrySharp.Internal{
	public class Request{
		public Request(Session session, string uri, string body){
			Session = session;
			Uri = new UriRequest(uri);
			Body = body;
		}

		public UriRequest Uri { get; private set; }
		public string Body { get; private set; }
		public Session Session { get; private set; }
	}
}