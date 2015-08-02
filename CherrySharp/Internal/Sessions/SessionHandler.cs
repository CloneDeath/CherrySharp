using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CherrySharp.Interfaces;

namespace CherrySharp.Internal.Sessions{
	public class SessionHandler{
		private const string SESSIONID_NAME = "sessionid";
		private readonly SessionCollection _sessions;

		public SessionHandler(ISessionConfiguration configuration){
			_sessions = new SessionCollection(configuration);
		}

		private static bool HasSessionCookie(CookieCollection collection){
			return collection.Cast<Cookie>().Any(cookie => cookie.Name == SESSIONID_NAME);
		}

		public List<Cookie> GetCookiesForRequest(CookieCollection collection){
			if (HasSessionCookie(collection)){
				return new List<Cookie>();
			}

			var session = _sessions.NewSession();
			return new List<Cookie>{
				new Cookie(SESSIONID_NAME, session.SessionId.ToString()){
					Expires = session.ExpirationDate,
				}
			};
		}

		public Session GetSession(CookieCollection cookies){
			var cookie = GetSessionCookie(cookies);
			if (cookie == null) return null;

			var guid = Guid.Parse(cookie.Value);

			return _sessions.GetSession(guid);
		}

		private static Cookie GetSessionCookie(CookieCollection cookies){
			return cookies.Cast<Cookie>().FirstOrDefault(cookie => cookie.Name == SESSIONID_NAME);
		}
	}
}