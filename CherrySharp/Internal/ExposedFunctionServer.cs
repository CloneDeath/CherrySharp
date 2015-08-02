using System;
using System.Linq;
using System.Reflection;
using CherrySharp.Interfaces;
using CherrySharp.Internal.UriParsing;
using Newtonsoft.Json;

namespace CherrySharp.Internal{
	public class ExposedFunctionServer : WebServer{
		private readonly ExposedFunctionCollection _collection;
		private readonly object _target;
		public ExposedFunctionServer(object target) : this(target, new Configuration()){}

		public ExposedFunctionServer(object target, IConfiguration configuration) : base(configuration){
			_target = target;
			_collection = new ExposedFunctionCollection(_target);
		}

		protected override string OnRequestReceived(string request){
			return GetResponseForRequest(request);
		}

		public string GetResponseForRequest(string request, string body){
			var uri = new UriRequest(request);

			var invoke = uri.Segments.Last();
			if (invoke == "") invoke = "Index";
			var method = _collection.GetMethod(invoke);

			var args = GetURLArguments(method, uri);

			if (!String.IsNullOrEmpty(body)){
				args[0] = JsonConvert.DeserializeObject(body, method.GetParameters()[0].ParameterType);
			}

			var response = method.Invoke(_target, args);

			return ProcessResponse(response);
		}

		private static object[] GetURLArguments(MethodInfo method, UriRequest uri){
			var methodParams = method.GetParameters();
			var args = new Object[methodParams.Count()];

			for (var i = 0; i < args.Length; i++){
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
			return args;
		}

		public string GetResponseForRequest(string request){
			return GetResponseForRequest(request, "");
		}

		private string ProcessResponse(object response){
			if (response is string) return response.ToString();

			return JsonConvert.SerializeObject(response);
		}
	}
}