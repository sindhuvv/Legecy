using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Tms.ScheduledJobRunner
{
	public class EmailJobScheduler
	{
		private IScheduler _scheduler; // after Start, and until shutdown completes, references the scheduler object

		// starts the scheduler, defines the jobs and the triggers
		public async Task Start()
		{
			if (_scheduler != null)
			{
				throw new InvalidOperationException("running....");
			}

			var properties = new NameValueCollection
			{
				// json serialization is the one supported under .NET Core (binary isn't)
				["quartz.serializer.type"] = "json",

				// the following setup of job store is just for example and it didn't change from v2
				["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
			};

			var schedulerFactory = new StdSchedulerFactory(properties);
			_scheduler = await schedulerFactory.GetScheduler();
			await _scheduler.Start();

			var userEmailsJob = JobBuilder.Create<EmailJob>()
				.WithIdentity("SendUserEmails")
				.Build();
			var userEmailsTrigger = TriggerBuilder.Create()
				.WithIdentity("UserEmailsCron")
				.StartNow()
				.WithCronSchedule("0/30 0/1 * 1/1 * ? *")
				.Build();

			await _scheduler.ScheduleJob(userEmailsJob, userEmailsTrigger);

		}

		// initiates shutdown of the scheduler, and waits until jobs exit gracefully (within allotted timeout)
		public async Task Stop()
		{
			if (_scheduler == null)
			{
				return;
			}

			await _scheduler.Shutdown(waitForJobsToComplete: true);
			_scheduler = null;
		}
	}
}
