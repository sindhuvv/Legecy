using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tms.Web.Interfaces;

namespace Tms.Web.Controllers.Api
{
	public class TrainingController : BaseApiController
	{
		private readonly ICachedTrainingService _trainingService;

		public TrainingController(ICachedTrainingService trainingService) => _trainingService = trainingService;

		[HttpGet]
		[Authorize(Policy = "TrainingViewers")]
		public async Task<IActionResult> List()
		{
			var catalogModel = await _trainingService.ListTrainings();
			return Ok(catalogModel);
		}

		[Route("SaveData/{tableName}/{columnName}/{colValue}/{id}")]
		public async Task<IActionResult> SaveData(string tableName, string columnName, string colValue, int id)
		{
			await _trainingService.SaveData(tableName, columnName, colValue, id);
			return Ok();
		}

		[Route("DeleteRecord/{tableName}/{id}")]
		public async Task<IActionResult> DeleteRecord(string tableName, int id)
		{
			await _trainingService.DeleteRecord(tableName, id);
			return Ok(true);
		}
	}
}
