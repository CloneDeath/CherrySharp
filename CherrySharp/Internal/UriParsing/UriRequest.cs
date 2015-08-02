namespace CherrySharp.Internal.UriParsing{
	public class UriRequest{
		private readonly UriParameters _parameters;

		public UriRequest(string request){
			if (request.StartsWith("/")) request = request.Substring(1);

			var halves = request.Split('?');

			Segments = halves[0].Split('/');

			var paramString = halves.Length > 1 ? halves[1] : "";
			_parameters = new UriParameters(paramString);
		}

		public string[] Segments { get; private set; }

		public string GetParameter(string name){
			return _parameters.GetParameter(name);
		}
	}
}