using System.Collections.Generic;
using CherrySharp.Internal;
using CherrySharp.Internal.UriParsing;
using FluentAssertions;
using NUnit.Framework;

namespace CherrySharp.Tests.Internal{
	[TestFixture]
	public class UriRequestTests{
		[Test]
		public void SegmentsAreParsedCorrectly(){
			var request = new UriRequest("Bomb");
			request.Segments.Should().BeEquivalentTo("Bomb");
		}

		[Test]
		public void SlashesAreUsedToDelimateSegments(){
			var request = new UriRequest("/Hello/World");
			request.Segments.Should().BeEquivalentTo("Hello", "World");
		}

		[Test]
		public void ParametersAreExcludedFromSegments(){
			var request = new UriRequest("/What/Is?time=0");
			request.Segments.Should().BeEquivalentTo("What", "Is");
		}

		[Test]
		public void ParametersAreParsed(){
			var request = new UriRequest("/Info?Name=12");
			request.GetParameter("Name").Should().Be("12");
			request.GetParameter("wowow").Should().BeNull();
		}

		[Test]
		public void CanGetParameter(){
			var request = new UriRequest("/?name=nick&time=4");
			request.GetParameter("name").Should().Be("nick");
			request.GetParameter("time").Should().Be("4");
		}
	}
}