using CherrySharp.Interfaces;
using CherrySharp.Internal;

namespace CherrySharp{
	public static class Quick{
		private static WebServer _server;

		public static int Port{
			get { return 8080; }
		}

		public static void Start(object root){
			Start(root, new Configuration());
		}

		public static void Start(object root, IConfiguration configuration){
			_server = new ExposedFunctionServer(root, configuration);
			_server.Start();
		}

		public static void Stop(){
			_server.Stop();
		}
	}
}