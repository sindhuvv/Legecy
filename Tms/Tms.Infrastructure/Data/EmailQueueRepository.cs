using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class EmailQueueRepository : BaseRepository<EmailQueue>, IEmailQueueRepository
	{
		public EmailQueueRepository(TmsContext tmsContext) : base(tmsContext)
		{

		}
	}
}
