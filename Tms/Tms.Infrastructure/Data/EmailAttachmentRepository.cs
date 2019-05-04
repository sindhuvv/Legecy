using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class EmailAttachmentRepository : BaseRepository<EmailAttachment>, IEmailAttachmentRepository
	{
		public EmailAttachmentRepository(TmsContext tmsContext) : base(tmsContext)
		{

		}
	}
}
