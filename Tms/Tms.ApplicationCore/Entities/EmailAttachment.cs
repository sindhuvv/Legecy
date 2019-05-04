using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tms.ApplicationCore.Models;

namespace Tms.ApplicationCore.Entities
{
	[Table("EmailAttachment")]
	public class EmailAttachment : BaseEntity
	{
		public string Filename { get; set; }

		public string ContentType { get; set; }

		public byte[] FileContents { get; set; }

		public int EmailQueueId { get; set; }
	}
}