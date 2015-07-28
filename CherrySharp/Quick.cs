using System;
using CherrySharp.Internal;

namespace CherrySharp{
	public static class Quick{
		private static WebServer _server;

		public static int Port{
			get { return 8080; }
		}

		public static void Start(object root){
			_server = new ExposedFunctionServer(root);
			_server.Start();
		}

		public static void Stop(){
			_server.Stop();
		}
	}
}