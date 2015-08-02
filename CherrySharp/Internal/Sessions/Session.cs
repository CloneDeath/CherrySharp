using System;

namespace CherrySharp.Internal.Sessions{
	public class Session{
		public Session(){
			SessionId = Guid.NewGuid();
		}

		public Guid SessionId { get; private set; }
		public DateTime ExpirationDate { get; set; }

		public bool IsExpired{
			get { return DateTime.Now >= ExpirationDate; }
		}
	}
}