using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailJobRunner
{
	public class EmailJobScheduler
	{
		private IScheduler _scheduler;

		public async Task Start()
		{
			_scheduler = await StdSchedulerFactory.GetDefaultScheduler();
			await _scheduler.Start();

			var userEmailsJob = JobBuilder.Create<SendEmailJob>()
				.WithIdentity("myJob", "group1")
				.Build();

			var userEmailsTrigger = TriggerBuilder.Create()
				.WithIdentity("myTrigger", "group1")
				.StartNow()
				.WithSimpleSchedule(x => x
				.WithIntervalInSeconds(40)
				.RepeatForever())
				.Build();

			await _scheduler.ScheduleJob(userEmailsJob, userEmailsTrigger);
		}

		public async Task Stop()
		{
			if (_scheduler == null)
			{
				return;
			}

			// give running jobs 30 sec (for example) to stop gracefully
			if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
			{
				_scheduler = null;
			}
			else
			{
				// jobs didn't exit in timely fashion - log a warning...
			}
		}
	}
}
