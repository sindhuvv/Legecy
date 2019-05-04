using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Interfaces;
using Tms.Web.Email.Model;
using Tms.Web.Helpers;
using Tms.Web.Interfaces;

namespace Tms.Web.Services
{
	public class EmailService : IEmailService
	{
		private IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private IRazorViewToStringRenderer _razorViewToStringRenderer;

		public EmailService(IUnitOfWork unitOfWork, IMapper mapper, IRazorViewToStringRenderer razorViewToStringRenderer)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_razorViewToStringRenderer = razorViewToStringRenderer;
		}

		async Task IEmailService.QueueEmail(BaseEmailViewModel model, IEnumerable<EmailAttachment> attachments)
		{
			try
			{
				var email = _mapper.Map<BaseEmailViewModel, EmailQueue>(model);
				email.SendOnDate = DateTime.Now;
				email.Body = JsonConvert.SerializeObject(model);

				var result = await _unitOfWork.EmailQueueRepository.AddAsync(email);

				attachments.ForEach(x =>
				{
					x.EmailQueueId = result.Id;
				});
				await _unitOfWork.EmailAttachmentRepository.AddAsync(attachments);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		async Task IEmailService.SendEmail()
		 {
			try
			{
				var listEmail = await _unitOfWork.EmailQueueRepository.ListAllAsync();
				var emailAttachments = await _unitOfWork.EmailAttachmentRepository.ListAllAsync();
				string SmtpServer = "smtpout.us.kworld.kpmg.com";
				foreach (var email in listEmail)
				{
					var message = new MimeMessage();
					var builder = new BodyBuilder();
					foreach (var item in email.ToList.FromDelimited(";"))
					 message.To.Add(new MailboxAddress(item));

					foreach (var item in email.BCCList.FromDelimited(";"))
						message.Bcc.Add(new MailboxAddress(item));

					foreach (var item in email.CCList.FromDelimited(";"))
						message.Cc.Add(new MailboxAddress(item));

					message.From.Add(new MailboxAddress(email.FromAddress));
					message.Subject = email.Subject;

					var attachments = emailAttachments.ToList().Where(x => x.EmailQueueId == email.Id);
					if (attachments.Any())
					{
						foreach (var attachment in attachments)
							builder.Attachments.Add(attachment.Filename, attachment.FileContents);
					}
					var filepath = email.EmailType.Split(".");
					var viewName = filepath[filepath.Length - 1].Replace("Model", string.Empty);
					var type = Type.GetType(email.EmailType);
					var model = JsonConvert.DeserializeObject(email.Body, type);

					var sb = new StringBuilder();
					sb.AppendLine("<h1>KPMG Header</h1>");
					sb.Append(await _razorViewToStringRenderer.RenderViewToStringAsync("Email/Views/" + viewName + ".cshtml", model));
					sb.AppendLine("KPMG Footer");
					builder.HtmlBody = sb.ToString();
					message.Body = builder.ToMessageBody();

					using (var emailClient = new SmtpClient())
					{
						emailClient.Connect(SmtpServer);
						emailClient.Send(message);
						emailClient.Disconnect(true);
					}
					((IEmailService)this).DequeueEmail(email, attachments);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		void IEmailService.DequeueEmail(EmailQueue email, IEnumerable<EmailAttachment> attachments)
		{
			try
			{
				_unitOfWork.EmailLogRepository.Add(_mapper.Map<EmailQueue, EmailLog>(email));
				if(attachments.Any())
					_unitOfWork.EmailAttachmentRepository.Delete(attachments);
				_unitOfWork.EmailQueueRepository.Delete(email);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
