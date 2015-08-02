using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CherrySharp.Tests{
	[TestFixture]
	public abstract class FullServerHarness{
		private Exception _startException;

		[SetUp]
		public void SetupServer(){
			Task.Factory.StartNew(() =>{
				try{
					Quick.Start(this);
				}
				catch (Exception ex){
					_startException = ex;
				}
			});
			Thread.Sleep(1000);
			AssertQuickStartHasNoExceptions();
		}

		private void AssertQuickStartHasNoExceptions(){
			if (_startException != null) throw _startException;
		}

		[TearDown]
		public void StopServer(){
			AssertQuickStartHasNoExceptions();
			Quick.Stop();
		}
	}
}