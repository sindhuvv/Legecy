
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore.Models;

namespace Tms.Web.Models.Excel
{
	public class ManageRolesExcelResponse
	{
		public ManageRolesExcelResponse()
		{
			Data = new List<SecurityRoleDetail>();
		}

		public IReadOnlyList<SecurityRoleDetail> Data { get; set; }
	}
}
