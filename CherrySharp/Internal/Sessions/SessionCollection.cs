using System;
using System.Collections.Generic;
using CherrySharp.Interfaces;

namespace CherrySharp.Internal.Sessions{
	public class SessionCollection{
		private readonly ISessionConfiguration _configuration;
		private readonly Dictionary<Guid, Session> _sessions = new Dictionary<Guid, Session>();

		public SessionCollection(ISessionConfiguration configuration){
			_configuration = configuration;
		}

		public Session NewSession(){
			var session = new Session(){
				ExpirationDate = DateTime.Now + _configuration.SessionLifespan,
			};
			_sessions[session.SessionId] = session;
			return session;
		}

		public Session GetSession(Guid guid){
			return _sessions.ContainsKey(guid) ? _sessions[guid] : null;
		}
	}
}