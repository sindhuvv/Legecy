using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;

namespace Tms.Web.Interfaces
{
	public interface IUtbService
	{
		SelectList SearchEmployee(string SearchValue);

		Employee GetEmployee(int upn);
	}
}
