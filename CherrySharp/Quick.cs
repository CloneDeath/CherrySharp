using System;
using System.IO;
using System.Net;

namespace CherrySharp{
	public static class Quick{
		private static bool _continueRunning;
		private static HttpListener _listener;

		public static int Port{
			get { return 8080; }
		}

		public static void Start(object root){
			_continueRunning = true;

			_listener = new HttpListener();
			_listener.Prefixes.Add(String.Format(@"http://+:{0}/", Port));
			_listener.Start();
			while (_continueRunning){
				HttpListenerContext context;
				try{
					context = _listener.GetContext();
				}
				catch (HttpListenerException){
					break;
				}

				var request = context.Request;
				var response = context.Response;

				response.StatusCode = (int)HttpStatusCode.OK;
				response.StatusDescription = HttpStatusCode.OK.ToString();

				TextWriter writer = new StreamWriter(response.OutputStream);
				writer.Write("Hello World");
				writer.Close();

				response.Close();
			}
		}

		public static void Stop(){
			_continueRunning = false;
			_listener.Close();
		}
	}
}