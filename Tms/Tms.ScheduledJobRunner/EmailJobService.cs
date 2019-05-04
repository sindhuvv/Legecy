using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Tms.ScheduledJobRunner
{
	public class EmailJobService : IHostedService
	{
		private EmailJobScheduler sc;

		public EmailJobService()
		{
			sc = new EmailJobScheduler();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await sc.Start();
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await sc.Stop();
		}
	}
}
