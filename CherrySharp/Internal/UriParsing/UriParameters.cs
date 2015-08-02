using System;
using System.Collections.Generic;

namespace CherrySharp.Internal.UriParsing{
	public class UriParameters{
		private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();

		public UriParameters(string parameters){
			var pairs = parameters.Split(new[]{"&"}, StringSplitOptions.RemoveEmptyEntries);

			foreach (var pair in pairs){
				var kvp = pair.Split('=');
				_parameters.Add(kvp[0], kvp[1]);
			}
		}

		public string GetParameter(string name){
			return _parameters.ContainsKey(name) ? _parameters[name] : null;
		}
	}
}