using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BeginnerExamples.Other
{
	class Interrupt
	{
		public static Thread sleeper;
		public static Thread waker;

		public void Process()
		{
			Console.WriteLine("Enter Main method");
			sleeper = new Thread(PutThreadToSleep);
			waker = new Thread(WakeThread);
			sleeper.Start();
			waker.Start();
			Console.WriteLine("Exiting Main method");
			Thread.Sleep(5000);
			Console.ReadLine();
		}

		private static void PutThreadToSleep()
		{
			for (int i = 0; i < 50; i++)
			{
				Console.Write(i + " ");
				if (i == 10 || i == 20 || i == 30)
				{
					try
					{
						Console.WriteLine("Sleep, Going to sleep at {0}", i);
						Thread.Sleep(20);
					}
					catch (ThreadInterruptedException e)
					{
						Console.WriteLine("Forcibly ");
					}
					Console.WriteLine("woken");
				}
			}
		}

		//thread waker threadStart
		private static void WakeThread()
		{

			for (int i = 51; i < 100; i++)
			{
				Console.Write(i + " ");

				if (sleeper.ThreadState == ThreadState.WaitSleepJoin)
				{
					Console.WriteLine("Interrupting sleeper");
					sleeper.Interrupt();
				}
			}
		}
	}
}
