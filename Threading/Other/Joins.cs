using System;
using System.Threading;

namespace BeginnerExamples.Join
{
	public class Join
	{
		public static Thread T1;
		public static Thread T2;

		public void StartProcess()
		{
			T1 = new Thread(First);
			T2 = new Thread(Second);
			T1.Name = "T1";
			T2.Name = "T2";
			T1.Start();
			T2.Start();
			Thread.Sleep(5000);
			Console.ReadLine();
		}

		private void First()
		{
			for (int i = 0; i < 5; i++)
			{
				Console.WriteLine("T1 state [{0}], T1 showing {1}",
						T1.ThreadState, i);
			}
			Console.WriteLine("T1 state [{0}] about to join T2 state [{1}]", T1.ThreadState, T2.ThreadState);
		}

		private void Second()
		{
			Console.WriteLine(
							 "T2 state [{0}] just about to Join, T1 state [{1}], CurrentThreadName={2}",
							 T2.ThreadState, T1.ThreadState,
							 Thread.CurrentThread.Name);
			//join T1
			T1.Join();

			Console.WriteLine(
								"T2 state [{0}] T2 just joined T1, T1 state [{1}], CurrentThreadName={2}",
								T2.ThreadState, T1.ThreadState,
								Thread.CurrentThread.Name);

			for (int i = 5; i < 10; i++)
			{
				Console.WriteLine(
						"T2 state [{0}], T1 state [{1}], CurrentThreadName={2} showing {3}",
						T2.ThreadState, T1.ThreadState,
						Thread.CurrentThread.Name, i);
			}

			Console.WriteLine(
					"T2 state [{0}], T1 state [{1}], CurrentThreadName={2}",
					T2.ThreadState, T1.ThreadState,
					Thread.CurrentThread.Name);
		}
	}
}
