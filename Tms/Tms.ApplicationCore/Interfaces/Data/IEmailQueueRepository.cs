using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface IEmailQueueRepository : IRepository<EmailQueue>, IAsyncRepository<EmailQueue>
	{

	}
}
