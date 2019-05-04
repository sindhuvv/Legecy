using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class EmailLogRepository : BaseRepository<EmailLog>, IEmailLogRepository
	{
		public EmailLogRepository(TmsContext tmsContext) : base(tmsContext)
		{

		}
	}
}
