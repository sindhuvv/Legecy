using System;

namespace Tms.ApplicationCore.Entities
{
	public class TrainingSession : BaseEntity
	{
		public int TrainingId { get; set; }

		public string SessionName { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Region { get; set; }

		public string BusinessUnitKey { get; set; }

		public string BusinessAreaKey { get; set; }

		public int MaxParticipants { get; set; }

		public bool IsActive { get; set; }

		public int RosterMaximum { get; set; }
	}
}