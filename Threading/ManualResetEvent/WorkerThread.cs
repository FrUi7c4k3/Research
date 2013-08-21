using System.Threading;
using BeginnerExamples.EventArgs;

namespace BeginnerExamples.Main
{
	public delegate void ReportWorkDoneEventhandler(object sender, WorkDoneCancelEventArgs e);

	internal class WorkerThread
	{
		private Thread worker;
		public event ReportWorkDoneEventhandler ReportWorkDone;
		private volatile bool cancel = false;
		private ManualResetEvent trigger = new ManualResetEvent(true);

		public void Start(long primeNumberLoopToFind)
		{
			worker = new Thread(DoWork);
			worker.Start(primeNumberLoopToFind);
		}

		private void DoWork(object data)
		{
			int divisorsFound = 0;
			int startDivisor = 1;
		
			long primeNumberLoopToFind = (long) data;

			for (int i = 0; i < primeNumberLoopToFind; i++)
			{
				//wait for trigger
				trigger.WaitOne();

				divisorsFound = 0;
				startDivisor = 1;

				//check for prime numbers, and if we find one raise
				//the ReportWorkDone event
				while (startDivisor <= i)
				{
					if (i%startDivisor == 0)
						divisorsFound++;
					startDivisor++;
				}

				if (divisorsFound == 2)
				{
					WorkDoneCancelEventArgs e = new WorkDoneCancelEventArgs(i);
					OnReportWorkDone(e);
					cancel = e.Cancel;

					//check whether thread should carry on, 
					//perhaps user cancelled it
					if (cancel)
						return;
				}
			}
		}

		/// <summary>
		/// make the worker thread wait on the ManualResetEvent
		/// </summary>
		public void Pause()
		{
			trigger.Reset();
		}

		/// <summary>
		/// signal the worker thread, raise signal on 
		/// the ManualResetEvent
		/// </summary>
		public void Resume()
		{
			trigger.Set();
		}

		/// <summary>
		/// Raise the ReportWorkDone event
		/// </summary>
		protected virtual void OnReportWorkDone(WorkDoneCancelEventArgs e)
		{
			if (ReportWorkDone != null)
			{
				ReportWorkDone(this, e);
			}
		}
	}
}
