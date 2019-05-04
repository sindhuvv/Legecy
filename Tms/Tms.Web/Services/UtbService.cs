using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using Tms.ApplicationCore.Entities;
using Tms.Infrastructure.Data;
using Tms.Web.Interfaces;

namespace Tms.Web.Services
{
	public class UtbService : IUtbService
	{
		private ITmsDapper _tmsDapper;

		public UtbService(ITmsDapper tmsDapper)
		{
			_tmsDapper = tmsDapper;
		}

		public Employee GetEmployee(int upn)
		{
			var employee = _tmsDapper.Query<Employee>(Sql.GetEmployeeDetailsByUpn,
				new
				{
					UPN = upn
				});

			return employee.Result.Any() ? employee.Result.First() : null;
		}

		public SelectList SearchEmployee(string SearchValue)
		{
			var upn = 0;
			if (Int32.TryParse(SearchValue, out upn))
			{
				var employees = _tmsDapper.Query<Employee>(Sql.GetEmployeeDetailsByUpn, new
				{
					UPN = upn
				});
				return new SelectList(employees.Result, "UPN", "HRName");
			}
			else
			{
				var employees = _tmsDapper.Query<Employee>(Sql.GetEmployeeDetailsByHRName, new
				{
					Searchvalue = SearchValue.Replace("'", "''")
				});
				var searchResult = employees.Result.Any() ? employees.Result.Take(150) : employees.Result;
				return new SelectList(searchResult, "UPN", "HRName");
			}
		}
	}
}
