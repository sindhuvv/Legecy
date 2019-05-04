using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Tms.ScheduledJobRunner
{
	internal class CustomWebHostService : WebHostService
	{
		private EmailJobScheduler sc;

		public CustomWebHostService(IWebHost host) : base(host)
		{
			sc = new EmailJobScheduler();
		}

		protected override void OnStarting(string[] args)
		{
			sc.Start().Wait();
			base.OnStarting(args);			
		}

		protected override void OnStarted()
		{
			base.OnStarted();			
		}

		protected override void OnStopping()
		{
			base.OnStopping();
			sc.Stop().Wait();
		}
	}



	public static class WebHostServiceExtensions
	{
		public static void RunAsCustomService(this Microsoft.AspNetCore.Hosting.IWebHost host)
		{
			var webHostService = new CustomWebHostService(host);
			ServiceBase.Run(webHostService);
		}
	}
}
