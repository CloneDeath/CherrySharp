using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CherrySharp.Interfaces;

namespace CherrySharp.Internal
{
	public class SessionHandler
	{
		private readonly ISessionConfiguration _configuration;

		public SessionHandler(ISessionConfiguration configuration){
			_configuration = configuration;
		}

		public List<Cookie> GetCookiesForRequest(CookieCollection collection){
			foreach (var cookie in collection.Cast<Cookie>()){
				if (cookie.Name == "sessionid"){
					return new List<Cookie>();
				}
			}

			return new List<Cookie>(){
				new Cookie("sessionid", "1"){
					Expires = DateTime.Now + _configuration.SessionLifespan,
				}
			};
		} 
	}
}
