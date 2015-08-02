using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CherrySharp.Tests
{
	public class IntSession : ISession{
		public int Value = 0;
	}

	[TestFixture]
	public class ExposeChecksForISessionTests : FullServerHarness
	{
		[Expose]
		public void SetInt(IntSession session, int value){
			session.Value = value;
		}

		public string GetInt(IntSession session){
			return session.Value.ToString();
		}

		[Test]
		public void SessionDataPersists(){
			
		}
	}
}
