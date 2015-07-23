using CherrySharp.Internal;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal{
	internal class MockProvider : BaseClass{
		[Expose]
		public string Index(){
			return "Hello World";
		}
	}

	internal class BaseClass{
		[Expose]
		public void Hidden(string command){}
	}

	[TestFixture]
	public class ExposedFunctionCollectionTests{
		private ExposedFunctionCollection _collector;

		[SetUp]
		public void SetupCollector(){
			_collector = new ExposedFunctionCollection(new MockProvider());
		}


		[Test]
		public void ExposeCollectorGetsAllFunctionsIncludingInherited(){
			_collector.Count.Should().Be(2);
		}

		[Test]
		public void CollectorProvidesMethodInfo(){
			_collector[0].Name.Should().Be("Index");
		}
	}
}