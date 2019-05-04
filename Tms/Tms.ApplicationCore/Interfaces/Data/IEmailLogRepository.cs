using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface IEmailLogRepository : IRepository<EmailLog>, IAsyncRepository<EmailLog>
	{

	}
}
