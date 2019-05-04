using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;
using Tms.Infrastructure.Data;
using Tms.Web.Interfaces;

namespace Tms.Web.Services
{
	public class TrainingService : ITrainingService
	{
		private IUnitOfWork _unitOfWork;
		private ITmsDapper _dapper;

		public TrainingService(IUnitOfWork unitOfWork, ITmsDapper dapper)
		{
			_unitOfWork = unitOfWork;
			_dapper = dapper;
		}

		async Task<Training> ITrainingService.GetTraining(int trainingId)
		{
			return await _unitOfWork.TrainingRepository.GetByExprAsync(x => x.Id == trainingId);
		}

		async Task<Training> ITrainingService.AddTraining(Training training)
		{
			await _unitOfWork.TrainingRepository.AddAsync(training);
			return training;
		}

		async Task<Training> ITrainingService.UpdateTraining(Training training)
		{
			await _unitOfWork.TrainingRepository.UpdateAsync(training);
			return training;
		}

		async Task<IReadOnlyList<Training>> ITrainingService.ListTrainings()
		{
			return await _unitOfWork.TrainingRepository.ListAllAsync();
		}

		async Task<IReadOnlyList<TrainingSession>> ITrainingService.ListSessions()
		{
			return await _unitOfWork.TrainingSessionRepository.ListAllAsync();
		}

		async Task ITrainingService.SaveData(string tableName, string columnName, object colValue, int id)
		{
			var sql = @"UPDATE dbo." + tableName + " SET " + columnName + " = @colValue WHERE ID = @id";
			await _dapper.QueryNonQuery(sql, new { colValue = colValue, id = id });
		}

		async Task ITrainingService.DeleteRecord(string tableName, int id)
		{
			var sql = @"DELETE FROM dbo." + tableName + " WHERE ID = @id";
			await _dapper.QueryNonQuery(sql, new { id = id });
		}
	}
}
