using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests{
	public class IntSession : ISession{
		public int Value;
	}

	[TestFixture]
	public class ExposeChecksForISessionTests : FullServerHarness{
		[Expose]
		public void SetInt(IntSession session, int value){
			session.Value = value;
		}

		[Expose]
		public string GetInt(IntSession session){
			return session.Value.ToString();
		}

		[Test]
		public void SessionDataPersists(){
			SendRequest("SetInt?value=12");

			var response = SendRequest("GetInt");
			response.Body.Should().Be("12");
		}
	}
}