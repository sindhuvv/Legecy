using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tms.Web.Interfaces;
using Tms.Web.ViewModels;

namespace Tms.Web.Controllers
{
	[Route("[controller]")]
	public class PocController : Controller
	{
		private ICachedTrainingService _cachedTrainingService;

		public PocController(ICachedTrainingService trainingService, ICachedSecurityService cachedSecurityService)
		{
			_cachedTrainingService = trainingService;
		}

		[Route("index")]
		public async Task<IActionResult> Index()
		{
            var model = new TrainingsViewModel
			{
				Trainings = await _cachedTrainingService.ListTrainings(),
				Sessions = await _cachedTrainingService.ListSessions(),
			};
            return View(model);
		}
	}
}
