using System;

namespace Tms.ApplicationCore.Entities
{
	public class SecurityEmployeeDelegation : BaseEntity
	{
		public int ParentUpn { get; set; }

		public int DelegateUpn { get; set; }

		public DateTime? EffectiveStartDate { get; set; }

		public DateTime? EffectiveEndDate { get; set; }

		public string Comments { get; set; }
	}
}