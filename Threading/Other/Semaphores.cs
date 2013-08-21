using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BeginnerExamples.Other
{
	public class Semaphores
	{
		/// <summary>
		/// The initial value refers to how many threads are allowed between the WaitOne and Release sections.
		/// The second value refers to the maximum allowed threads to ever be allowed between the above mentioned sections.
		/// eg. Calling Release(2) would up the currently 3 allowed to 4. Calling it again will set it to 5. Thus five will be allowed
		/// to run at a time between those sections. If called again an exception will be thrown. This is a mechanism for thread pooling!:)
		/// </summary>
		static Semaphore sem = new Semaphore(3, 5);

		public static void Start()
		{
			for (int i = 0; i < 10; i++)
			{
				new Thread(RunThread).Start("T" + i);
			}

			Console.ReadLine();
		}

	
		static void RunThread(object threadID)
		{
			while (true)
			{
				Console.WriteLine(string.Format(
						"thread {0} is waiting on Semaphore",
						threadID));
				sem.WaitOne();

				try
				{
					Console.WriteLine(string.Format(
							"thread {0} is in the Semaphore, and is now Sleeping",
							threadID));
					Thread.Sleep(100);
					Console.WriteLine(string.Format(
							"thread {0} is releasing Semaphore",
							threadID));
				}
				finally
				{
					//Allow another into the Semaphore
					sem.Release();
				}
			}
		}
	}
}
