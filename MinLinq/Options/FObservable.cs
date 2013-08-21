using System;

namespace MinLinq.Options
{
	public class FObservable
	{
		public static Action<Action<Option<T>>> Empty<T>()
		{
			return o => o(new Option<T>.None());
		}

		public static Action<Action<Option<T>>> Return<T>(T value)
		{
			return o =>
			{
				o(new Option<T>.Some(value));
				o(new Option<T>.None());
			};
		}
	}

	//*********************************************************************
	// Explanation
	//*********************************************************************
	// IObserver
	//						----------------
	//						| OnNext(T)
	//		T	 ->		| OnComplete(T)
	//						----------------
	// ie. T => ()
	// ie. Action<T>

	// IObservable
	//									-------------
	// IObserver<T>	=>	| Subscribe(IObserver<T>)
	//									-------------
	// ie. (T => ()) => () | Subscribe(T => ()) | T => () => ()
	// ie. Action<T> => ()
	// ie. Action<Action<T>> //
	//*************************
	//*********************************************************************
}
