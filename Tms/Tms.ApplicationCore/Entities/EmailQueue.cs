using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tms.ApplicationCore.Entities
{
	[Table("EmailQueue")]
	public class EmailQueue : BaseEntity
	{	
		public string FromAddress { get; set; }

		public string ToList { get; set; }

		public string CCList { get; set; }

		public string BCCList { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public string BatchId { get; set; }

		public DateTime SendOnDate { get; set; }

		public string EmailType { get; set; }	
	}
}