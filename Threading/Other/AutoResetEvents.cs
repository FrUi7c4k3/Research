
using System.Threading;
using System;
namespace BeginnerExamples.ResetEvents
{
	class AutoReset
	{
		public static Thread T1;
		public static Thread T2;
		//This AutoResetEvent starts out non-signalled
		public static AutoResetEvent are = new AutoResetEvent(false);

		static void AutomaticResetExample()
		{
			T1 = new Thread(() =>
												{
													Console.WriteLine(
														"T1 is simulating some work by sleeping for 5 secs");
													//calling sleep to simulate some work
													Thread.Sleep(5000);
													Console.WriteLine(
														"T1 is just about to set AutoResetEvent are");
													//alert waiting thread(s)
													are.Set(); // AutoReset resets immediately after the first thread is allowed to pass.
													Thread.Sleep(5000);
													are.Set();
												});

			T2 = new Thread(() =>
											{
												//wait for AutoResetEvent ar1, this will wait for ar1 to be signalled
												//from some other thread
												Console.WriteLine(
													"T2 starting to wait for AutoResetEvent ar1, at time {0}",
													DateTime.Now.ToLongTimeString());
												are.WaitOne();
												Console.WriteLine(
													"T2 finished waiting for AutoResetEvent ar1, at time {0}",
													DateTime.Now.ToLongTimeString());

												//wait for AutoResetEvent ar2, this will skip straight through
												//as AutoResetEvent ar2 started out in the signalled state
												Console.WriteLine(
													"T2 starting to wait for AutoResetEvent are, at time {0}",
													DateTime.Now.ToLongTimeString());
												are.WaitOne();
												Console.WriteLine(
													"T2 finished waiting for AutoResetEvent are, at time {0}",
													DateTime.Now.ToLongTimeString());
											});

			T1.Name = "T1";
			T2.Name = "T2";
			T1.Start();
			T2.Start();
			Console.ReadLine();
		}

		public static Thread CreateOrderThread;
		public static Thread SaveOrderThread;
		public static Thread PrintOrderThread;
		//This AutoResetEvent starts out non-signalled
		public static AutoResetEvent ar1 = new AutoResetEvent(false);
		public static AutoResetEvent ar2 = new AutoResetEvent(false);

		static void FlowControlExample()
		{
			CreateOrderThread = new Thread((ThreadStart)delegate
			{
				Console.WriteLine(
						"CreateOrderThread is creating the Order");
				//calling sleep to simulate some work
				Thread.Sleep(5000);
				//alert waiting thread(s)
				ar1.Set();
			});

			SaveOrderThread = new Thread((ThreadStart)delegate
			{
				//wait for AutoResetEvent ar1, this will wait for ar1
				//to be signalled from some other thread
				ar1.WaitOne();
				Console.WriteLine(
						"SaveOrderThread is saving the Order");
				//calling sleep to simulate some work
				Thread.Sleep(5000);
				//alert waiting thread(s)
				ar2.Set();
			});

			PrintOrderThread = new Thread((ThreadStart)delegate
			{
				//wait for AutoResetEvent ar1, this will wait for ar1
				//to be signalled from some other thread
				ar2.WaitOne();
				Console.WriteLine(
						"PrintOrderThread is printing the Order");
				//calling sleep to simulate some work
				Thread.Sleep(5000);
			});

			CreateOrderThread.Name = "CreateOrderThread";
			SaveOrderThread.Name = "SaveOrderThread";
			PrintOrderThread.Name = "PrintOrderThread";
			CreateOrderThread.Start();
			SaveOrderThread.Start();
			PrintOrderThread.Start();
			Console.ReadLine();
		}
	}
}
