using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;

namespace Tms.Web.Interfaces
{
	public interface ICachedTrainingService
	{
		Task<IReadOnlyList<Training>> ListTrainings();
		Task<IReadOnlyList<TrainingSession>> ListSessions();
		Task SaveData(string tableName, string columnName, object colValue, int id);
		Task DeleteRecord(string tableName, int id);
	}
}
