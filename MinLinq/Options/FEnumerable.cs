using System;

namespace MinLinq.Options
{
	/// <summary>
	/// From IEnumerable - a functional representation
	/// </summary>
	public class FEnumerable
	{
		public static Func<Func<Option<T>>> Empty<T>()
		{
			return () => () => new Option<T>.None();
		}

		public static Func<Func<Option<T>>> Return<T>(T value)
		{
			return () =>
			{
				int i = 0;
				return () =>
						i++ == 0
						? (Option<T>)new Option<T>.Some(value)
						: (Option<T>)new Option<T>.None();
			};
		}
	}
	
	//*********************************************************************
	// Explanation
	//*********************************************************************
	// IEnumerator
	//              ------------------
	//              | MoveNext():bool
	// object  <-		| Current:object
	//              ------------------
	// ie. () => object
	// ie. Func<object>

	// IEnumerable
	// IEnumerator <- GetEnumerator() | GetEnumerator():IEnumerator
	// ie. () => IEnumerator
	// ie. () => () => object					| () –> (() –> object)
	// ie. () => Func<object>
	// ie. Func<Func<object> !!:) //
	//****************************//
	//*********************************************************************
}
