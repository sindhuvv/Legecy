using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.Infrastructure.Caching;
using Tms.Infrastructure.Logging;
using Tms.Web.Interfaces;

namespace Tms.Web.Services
{
	public class CachedTrainingService : ICachedTrainingService
	{
		public ITmsLogger _logger;
		public ITmsCache _cache;
		private ITrainingService _trainingService;

		#region cache Keys
		private const string ListTrainings_CacheKey = "list-trainings";
		private const string ListSessions_CacheKey = "list-sessions";
		#endregion

		public CachedTrainingService(ITrainingService trainingService, ITmsCache cache, ITmsLogger logger)
		{
			_trainingService = trainingService;
			_cache = cache;
			_logger = logger;
		}

		async Task<IReadOnlyList<Training>> ICachedTrainingService.ListTrainings()
		{
			_logger.LogInfo("ListTrainings Start ");

			var cache_key = ListTrainings_CacheKey;
			var trainings = await _cache.GetAsync<IReadOnlyList<Training>>(cache_key);
			if (trainings == null)
			{
				trainings = await _trainingService.ListTrainings();
				await _cache.SetAsync(cache_key, trainings);
			}

			_logger.LogInfo("ListTrainings End ");
			_logger.LogError("ListTrainings : Testing error logging."); //TODO: cleanup

			return trainings;
		}

		async Task<IReadOnlyList<TrainingSession>> ICachedTrainingService.ListSessions()
		{
			var cache_key = ListSessions_CacheKey;
			var trainings = await _cache.GetAsync<IReadOnlyList<TrainingSession>>(cache_key);
			if (trainings == null)
			{
				trainings = await _trainingService.ListSessions();
				await _cache.SetAsync(cache_key, trainings);
			}
			return trainings;
		}

		async Task ICachedTrainingService.SaveData(string tableName, string columnName, object colValue, int id)
		{
			await _trainingService.SaveData(tableName, columnName, colValue, id);
		}

		async Task ICachedTrainingService.DeleteRecord(string tableName, int id)
		{
			await _trainingService.DeleteRecord(tableName, id);
		}
	}
}
