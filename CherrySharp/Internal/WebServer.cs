using System;
using System.IO;
using System.Net;
using CherrySharp.Interfaces;

namespace CherrySharp.Internal{
	public abstract class WebServer{
		private readonly IServerConfiguration _configuration;
		private readonly SessionHandler _sessionHandler;
		private HttpListener _listener;

		protected WebServer(IConfiguration configuration){
			_configuration = configuration;
			_sessionHandler = new SessionHandler(configuration);
		}

		protected abstract string OnRequestReceived(string request);

		public void Start(){
			_listener = new HttpListener();
			_listener.Prefixes.Add(String.Format(@"{0}://+:{1}/", _configuration.Protocol, _configuration.Port));
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

				foreach (var cookie in _sessionHandler.GetCookiesForRequest(request.Cookies)){
					response.AppendCookie(cookie);
				}

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