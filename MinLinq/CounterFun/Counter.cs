using System;
using MinLinq.Interfaces;

//interfaces are essentially nothing but records of functions. And functions, as we all know, are the fundamental pillars of functional programming languages
//Interfaces with a single method have a name: they’re delegates!!!
namespace MinLinq
{
	public class Counter : IRecord<Func<int>, Action<int>>
	{
		private int _value;

		public Func<int> First { get { return () => _value; } }

		public Action<int> Second { get { return i => _value += i; } }
	}

	public class ExplicitCounter : IRecord<Func<int>, Action<int>>
	{
		private int _value;

		// Because these properties are declared explicitly they now automatically become private!!!:) And Interface is still satisfied!!! Woohooo!!! :) :) :)
		Func<int> IRecord<Func<int>, Action<int>>.First { get { return () => _value; } }

		Action<int> IRecord<Func<int>, Action<int>>.Second
		{
			get { return i => _value += i; }
		}

		// To directly access private members from the outside, 
		// explicitly cast 'this' and then access the private from there which is allowed because you're in the scope of the instance!!! :) Waha! Totally cheat cool!
		public Func<int> Get { get { return ((IRecord<Func<int>, Action<int>>)this).First; } }  // public Func<int> Get { get { return this.First; } } won't work!!! You must do an explicit cast of this to return a private first!
		public Action<int> Inc { get { return ((IRecord<Func<int>, Action<int>>)this).Second; } }
	}
}
