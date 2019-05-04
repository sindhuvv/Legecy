using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;
using Tms.Web.Interfaces;

namespace Tms.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtbController : ControllerBase
    {
		private IUtbService _baseService;

		public UtbController(IUtbService baseService)
		{
			_baseService = baseService;
		}

		//TODO: Search query needs to updated when complete employee table to migrated to TMS.
		[HttpGet("SearchEmployee/{SearchValue}")]
		public SelectList SearchEmployee([FromRoute]string SearchValue)
		{
			return _baseService.SearchEmployee(SearchValue);
		}
	}
}