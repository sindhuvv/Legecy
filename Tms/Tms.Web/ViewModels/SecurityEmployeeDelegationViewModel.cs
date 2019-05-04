using System;
using System.ComponentModel.DataAnnotations;

namespace Tms.Web.ViewModels
{
	public class SecurityEmployeeDelegationViewModel
	{
		public int? ParentUpn { get; set; }

		[Required]
		public int? DelegateUpn { get; set; }

		[Required]
		public DateTime? EffectiveStartDate { get; set; }

		[Required]
		public DateTime? EffectiveEndDate { get; set; }

		public string Comments { get; set; }
	}
}