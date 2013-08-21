using System.ComponentModel;
using System.Collections.Generic;

namespace BeginnerExamples.EventArgs
{
	public class WorkDoneCancelEventArgs : CancelEventArgs
	{
		public static List<int> PrimesFound = new List<int>();
		public int PrimeFound { get; private set; }

		public WorkDoneCancelEventArgs(int primeFound)
		{
			PrimeFound = primeFound;
			PrimesFound.Add(primeFound);
		}
	}
}
