using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class TrainingSessionRepository : BaseRepository<TrainingSession>, ITrainingSessionRepository
	{
		public TrainingSessionRepository(TmsContext tmsContext) : base(tmsContext)
		{

		}
	}
}
