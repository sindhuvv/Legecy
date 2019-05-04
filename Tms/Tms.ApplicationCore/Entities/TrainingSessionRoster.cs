using Tms.ApplicationCore.Models;

namespace Tms.ApplicationCore.Entities
{
	public class TrainingSessionRoster : BaseEntity
	{
		public int TrainingSessionId { get; set; }

		public int UPN { get; set; }

		public RosterStatus Status { get; set; }

		public string Notes { get; set; }
	}
}