using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherrySharp.Internal
{
	public class UriRequest
	{
		public UriRequest(string request){
			if (request.StartsWith("/")) request = request.Substring(1);

			var halves = request.Split('?');

			Segments = halves[0].Split('/');
			Parameters = new Dictionary<string, string>();

			if (halves.Length > 1){
				var pairs = halves[1].Split(new []{"&"}, StringSplitOptions.RemoveEmptyEntries);

				foreach (var pair in pairs){
					var kvp = pair.Split('=');
					Parameters.Add(kvp[0], kvp[1]);
				}
			}
		}

		public string[] Segments { get; private set; }
		public Dictionary<string, string> Parameters { get; private set; }

		public string GetParameter(string name){
			if (!Parameters.ContainsKey(name)) return null;

			return Parameters[name];
		}
	}
}
