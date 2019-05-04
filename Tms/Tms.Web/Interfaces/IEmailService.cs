using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.Web.Email.Model;

namespace Tms.Web.Interfaces

{
	public interface IEmailService
	{
		void DequeueEmail(EmailQueue email, IEnumerable<EmailAttachment> attachments);
		Task SendEmail();
		Task QueueEmail(BaseEmailViewModel email, IEnumerable<EmailAttachment> attachments);
	}
}
