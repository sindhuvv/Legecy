using Tms.ApplicationCore.Models;

namespace Tms.ApplicationCore.Entities
{
	public class Training : BaseEntity
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public TrainingPopulationKey? PopulationKey { get; set; }

		public bool IsActive { get; set; }

		public int TrainingYear { get; set; }
	}
}