using System;
using System.IO;
using System.Net;

namespace CherrySharp.Internal{
	public abstract class WebServer{
		private HttpListener _listener;

		protected WebServer(){
			Port = 8080;
		}

		public int Port { get; set; }
		public object UserData { get; set; }
		protected abstract string OnRequestReceived(string request);

		public void Start(){
			_listener = new HttpListener();
			_listener.Prefixes.Add(String.Format(@"http://+:{0}/", Port));
			_listener.Start();

			while (true){
				HttpListenerContext context;
				try{
					context = _listener.GetContext();
				}
				catch (HttpListenerException){
					break;
				}

				var request = context.Request;
				var response = context.Response;

				response.StatusCode = (int) HttpStatusCode.OK;
				response.StatusDescription = HttpStatusCode.OK.ToString();

				using (var writer = new StreamWriter(response.OutputStream)){
					var responseBody = OnRequestReceived(request.RawUrl) ?? "";
					writer.Write(responseBody);
				}

				response.Close();
			}
		}

		public void Stop(){
			_listener.Close();
		}
	}
}