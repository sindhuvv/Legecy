using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface IEmailAttachmentRepository : IRepository<EmailAttachment>, IAsyncRepository<EmailAttachment>
	{

	}
}
