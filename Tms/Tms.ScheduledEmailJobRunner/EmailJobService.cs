using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EmailJobRunner
{
	public partial class EmailJobService : ServiceBase
	{
		private EmailJobScheduler sc;

		public EmailJobService()
		{
			InitializeComponent();
			sc = new EmailJobScheduler();
		}

		protected override void OnStart(string[] args)
		{
			sc.Start().Wait();
		}

		protected override void OnStop()
		{
			sc.Stop().Wait();
		}
	}
}
