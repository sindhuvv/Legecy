using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tms.ApplicationCore.Models;

namespace Tms.ApplicationCore.Entities
{
	[Table("EmailLog")]
	public class EmailLog : BaseEntity
	{
		public string FromAddress { get; set; }

		public string ToList { get; set; }

		public string CCList { get; set; }

		public string BCCList { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public DateTime SendOnDate { get; set; }
	}
}