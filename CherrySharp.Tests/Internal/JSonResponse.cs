using System.Runtime.Serialization;
using CherrySharp.Tests.Internal.Harnesses;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal{
	[DataContract]
	public class MyObject{
		[DataMember] public string Name = "Peter";
		[DataMember] public int Score;
	}

	[TestFixture]
	public class JSonResponse : ServerTestHarness{
		[Expose]
		public MyObject GetObject(){
			return new MyObject{
				Score = 3,
				Name = "Peter"
			};
		}

		[Expose]
		public void PushJson(MyObject obj){
			obj.Name.Should().Be("Michael");
			obj.Score.Should().Be(139);
		}

		[Test]
		public void GetJSon(){
			GetResponseForRequest("GetObject").Should().Be("{\"Name\":\"Peter\",\"Score\":3}");
		}

		[Test]
		public void PutJSonWorks(){
			GetResponseForRequest("PushJson", "{\"Score\":139,\"Name\":\"Michael\"}");
		}
	}
}