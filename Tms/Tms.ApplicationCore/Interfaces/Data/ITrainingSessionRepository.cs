using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface ITrainingSessionRepository : IRepository<TrainingSession>, IAsyncRepository<TrainingSession>
	{

	}
}
