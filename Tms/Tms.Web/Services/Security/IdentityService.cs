using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Interfaces;
using Tms.Infrastructure.Data;

namespace Tms.Web.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ITmsDapper _dapper;

		public IdentityService(IHttpContextAccessor httpContextAccessor, ITmsDapper dapper)
		{
			_httpContextAccessor = httpContextAccessor;
			_dapper = dapper;
		}

		async Task<int> IIdentityService.GetUserUpn()
		{
			var upn = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Cookies[TmsConstants.Impersonation_Cookie_Name]);
			if (upn == 0)
			{
				//TODO: Create DataStore method to get the employee details from cache
				var result = await _dapper.Query<int>(Sql.GetEmployeeUpnByDomainLogin, new { DomainLogin = _httpContextAccessor.HttpContext.User.Identity.Name });
				return result.Single();
			}
			return upn;
		}
	}
}
