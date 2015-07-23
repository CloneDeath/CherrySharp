using CherrySharp.Internal;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal{
	[TestFixture]
	public class ExposedFunctionCollectionTests{
		[Test]
		public void ExposeCollectorGetsAllFunctionsIncludingInherited(){
			var collector = new ExposedFunctionCollection(new MockProvider());
			collector.Count.Should().Be(2);
		}
	}

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
}