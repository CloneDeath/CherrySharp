using System;
using System.Runtime.Serialization;
using CherrySharp.Tests.Internal.Harnesses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal
{
	[DataContract]
	public class MyObject{

		[DataMember] public int Score = 0;

		[DataMember] public string Name = "Peter";
	}

	[TestFixture]
	public class JSonResponse : ServerTestHarness
	{
		[Expose]
		public MyObject GetObject(){
			return new MyObject(){
				Score = 3,
				Name = "Peter",
			};
		}

		[Expose]
		public void PushJson(MyObject obj){
			obj.Name.Should().Be("Michael");
			obj.Score.Should().Be(139);
		}

		[Test]
		public void GetJSon(){
			GetResponseForRequest("GetObject").Should().Be("{\"Score\":3,\"Name\":\"Peter\"}");
		}

		[Test]
		public void PutJSonWorks(){
			GetResponseForRequest("PushJson", "{\"Score\":139,\"Name\":\"Michael\"}")
		}
	}
}
