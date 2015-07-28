using System;
using System.Linq;
using System.Reflection;

namespace CherrySharp.Internal{
	public class ExposedFunctionServer : WebServer{
		private readonly ExposedFunctionCollection _collection;
		private readonly object _target;

		public ExposedFunctionServer(object target){
			_target = target;
			_collection = new ExposedFunctionCollection(_target);
		}

		protected override string OnRequestReceived(string request){
			return GetResponseForRequest(request);
		}

		public string GetResponseForRequest(string request){
			UriRequest uri = new UriRequest(request);

			var invoke = uri.Segments.Last();
			if (invoke == "") invoke = "Index";
			var method = _collection.GetMethod(invoke);

			var methodParams = method.GetParameters();
			var args = new Object[methodParams.Count()];

			for (int i = 0; i < args.Length; i++){
				var param = methodParams[i];

				var value = uri.GetParameter(param.Name);
				if (value == null){
					args[i] = param.DefaultValue;
				}
				else{
					if (param.ParameterType == typeof (int)){
						args[i] = int.Parse(value);
					}
					else if (param.ParameterType == typeof (string)){
						args[i] = value;
					}
					else{
						args[i] = value;
					}
				}
			}

			var response = method.Invoke(_target, args);
			return response.ToString();
		}
	}
}