using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tms.ScheduledJobRunner
{
	public class EmailJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			var clientHandler = new HttpClientHandler()
			{
				Credentials = CredentialCache.DefaultNetworkCredentials
			};
			var client = new HttpClient(clientHandler);
			client.BaseAddress = new Uri("http://localhost:64581/");
			await client.GetAsync("api/Home/SendEmail");
		}
	}
}
