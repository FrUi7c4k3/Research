using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BeginnerExamples.Other
{
	class ReaderWriterLockSlims
	{
		static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
		static List<int> items = new List<int>();
		static Random rand = new Random();

		public static void Start()
		{
			//start some readers
			new Thread(Read).Start("R1");
			new Thread(Read).Start("R2");
			new Thread(Read).Start("R3");

			//start some writers
			new Thread(Write).Start("W1");
			new Thread(Write).Start("W2");
		}

		static void Read(object threadID)
		{
			//do read
			while (true)
			{
				try
				{
					rw.EnterReadLock();
					Console.WriteLine("Thread " + threadID +
							" reading common source");
					foreach (int i in items)
						Thread.Sleep(10);
				}
				finally
				{
					rw.ExitReadLock();
				}
			}
		}


		static void Write(object threadID)
		{
			//do write
			while (true)
			{
				int newNumber = GetRandom(100);
				try
				{
					rw.EnterWriteLock();
					items.Add(newNumber);
				}
				finally
				{
					rw.ExitWriteLock();
					Console.WriteLine("Thread " + threadID +
							" added " + newNumber);
					Thread.Sleep(100);
				}
			}
		}

		static int GetRandom(int max)
		{
			//lock on the Random object
			lock (rand)
				return rand.Next(max);
		}
	}
}
