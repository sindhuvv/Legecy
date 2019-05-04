using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
	{
		public TrainingRepository(TmsContext tmsContext) : base(tmsContext)
		{

		}
	}
}
