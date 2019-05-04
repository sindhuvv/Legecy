using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface ITrainingRepository : IRepository<Training>, IAsyncRepository<Training>
	{

	}
}
