using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinLinq;

namespace MinLinqTests
{
	[TestClass]
	public class CounterTests
	{
		[TestMethod]
		public void PublicPropertiesAsFunctions()
		{
			Counter counter = new Counter();

			// Assert number is returned when called as a function / method
			Assert.AreEqual(0, counter.First());
			// Assert Func<int> is returned when called as a regular property
			Assert.AreEqual(typeof(Func<int>), counter.First.GetType());

			// Add 1
			counter.Second(1);
			Assert.AreEqual(1, counter.First());
			// Add 2
			counter.Second(2);
			Assert.AreEqual(3, counter.First());
		}

		[TestMethod]
		public void ExplicitPropertiesAreHidden()
		{
			ExplicitCounter counter = new ExplicitCounter();

			Assert.AreEqual(0, counter.Get()); // counter.First() / counter.First NOT accessable !!!
			
			Assert.AreEqual(typeof(Func<int>), counter.Get.GetType());
			Func<int, string> g;
			// Add 1
			counter.Inc(1);
			Assert.AreEqual(1, counter.Get());
			// Add 2
			counter.Inc(2);
			Assert.AreEqual(3, counter.Get());
		}

		[TestMethod]
		public void IncrementCounterInLoop()
		{
			for (ExplicitCounter counter = new ExplicitCounter(); counter.Get() < 10; counter.Inc(1))
			{
				Console.Out.WriteLine(counter.Get());
			}
		}
	}
}
