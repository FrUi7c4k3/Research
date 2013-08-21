using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BeginnerExamples.EventArgs;

namespace BeginnerExamples.Main
{
	public partial class Client : Form
	{
		private WorkerThread wt = new WorkerThread();
		private SynchronizationContext context;
		private bool primeThreadCancel = false;

		public Client()
		{
			InitializeComponent();

			context = SynchronizationContext.Current;
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			wt.Start(100000);
			wt.ReportWorkDone += new ReportWorkDoneEventhandler(wt_ReportWorkDone);
			primeThreadCancel = false;

		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			wt.Pause();
		}

		private void btnResume_Click(object sender, System.EventArgs e)
		{
			wt.Resume();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			primeThreadCancel = true;
		}

		void wt_ReportWorkDone(object sender, WorkDoneCancelEventArgs e)
		{
			//marshal call to UI thread
			context.Post((obj) => lstView.Items.Add(e.PrimeFound.ToString()) , null);

			//should worker thread be cancelled, has user clicked cancel button?
			e.Cancel = primeThreadCancel;

			//+++++++++++++++++++++++++++++++++++++++++++++++++++++
			//NOTE : This would also work to marshal call to UI thread
			//+++++++++++++++++++++++++++++++++++++++++++++++++++++

			//this.Invoke(new EventHandler(delegate
			//{
			//    lstItems.Items.Add(e.PrimeFound.ToString());
			//}));
		}


	}
}
