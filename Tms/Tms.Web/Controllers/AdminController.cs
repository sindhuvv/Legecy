using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore.Models;
using Tms.Infrastructure.Export;
using Tms.Web.ActionResults;
using Tms.Web.Attributes;
using Tms.Web.Interfaces;
using Tms.Web.Models.Excel;
using Tms.Web.ViewModels;

namespace Tms.Web.Controllers
{
	[Route("[controller]")]
	public class AdminController : Controller
	{
		private ICachedTrainingService _cachedTrainingService;
		private ICachedSecurityService _cachedSecurityService;
		private readonly IMapper _mapper;
		private IOfficeDocumentGenerator _officeDocumentGenerator;
		private IWorksheetDataBinder _worksheetDataBinder;
		private ISecurityService _securityService;

		public AdminController(ICachedTrainingService trainingService, ICachedSecurityService cachedSecurityService, IMapper mapper
			, IOfficeDocumentGenerator officeDocumentGenerator, IWorksheetDataBinder worksheetDataBinder, ISecurityService securityService)
		{
			_cachedTrainingService = trainingService;
			_cachedSecurityService = cachedSecurityService;
			_mapper = mapper;
			_officeDocumentGenerator = officeDocumentGenerator;
			_worksheetDataBinder = worksheetDataBinder;
			_securityService = securityService;
		}

		[AuthorizeIgnoreDelegates(Policy: "Trainers")]
		[Route("manage-roles")]		
		public async Task<IActionResult> ManageRoles(string authorizedAction = null, int? roleId = null)
		{
			var list = await _cachedSecurityService.ListAllSecurityRoleDetails();

			var model = new ManageRolesViewModel()
			{
				AllRoles = _mapper.Map<List<SecurityRoleDetail>, List<SecurityRoleViewModel>>(list.ToList())
			};
			model.Roles = model.AllRoles.Where(x =>
				(String.IsNullOrEmpty(authorizedAction) || x.IsAuthorized(authorizedAction)) &&
				(roleId == null || x.Id == roleId.Value)).ToList();

			return View(model);
		}

		[HttpGet]
		[Route("edit-role")]
		public async Task<IActionResult> EditRole(int? id)
		{
			var model = new SecurityUserRoleViewModel();
			if (id.HasValue)
			{
				model = await _cachedSecurityService.GetSecurityRoleDetails(id.Value);
			}
			return View(model);
		}

		[HttpPost]
		[Route("edit-role")]
		public async Task<IActionResult> EditRole(SecurityUserRoleViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var roleId = model.Id;
			if (!roleId.HasValue)
				roleId = await _cachedSecurityService.CreateNewRole(model);
			else
				await _cachedSecurityService.SaveRoleDetails(model);
			return RedirectToAction(nameof(EditRole), new { id = roleId });
		}

		[HttpPost]
		[Route("add-user-role")]
		public async Task<IActionResult> AddUserRole(int roleId, int upn)
		{
			await _cachedSecurityService.AddSecurityUserRole(roleId, upn);
			return Json(new { upn = upn });
		}

		[HttpPost]
		[Route("delete-user-role")]
		public async Task<IActionResult> DeleteUserRole(int roleId, int upn)
		{
			await _cachedSecurityService.DeleteSecurityUserRole(roleId, upn);
			return Json(new { upn = upn });
		}

		[HttpPost]
		[Route("delete-role")]
		public async Task<IActionResult> DeleteRole(int roleId)
		{
			await _cachedSecurityService.DeleteSecurityRole(roleId);
			return Json(null);
		}

		[HttpPost]
		[Authorize(Policy = "Trainers")]
		[Route("export-roles")]
		public async Task<ExcelResult> ExportRoles()
		{
			var roles = await _cachedSecurityService.ListAllSecurityRoleDetails();
			var workbook = _officeDocumentGenerator.OpenWorkbook("Tms.Web.ExcelTemplates.ManageRoles.xlsx");
			workbook.Worksheets[0].Name = "Manage Roles";
			var data = new ManageRolesExcelResponse { Data = roles.ToList() };
			_worksheetDataBinder.BindData(workbook.Worksheets[0], data);
			return new ExcelResult(workbook, "ManageRoles");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet]
		[Route("impersonate")]
		[Authorize(Policy = "Trainers&RosterAdmin")]		
		public ActionResult Impersonate()
		{
			return View(new ImpersonateViewModel());
		}

		[HttpPost]
		[Route("impersonate")]
		public ActionResult Impersonate(ImpersonateViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			_securityService.Impersonate(model.Upn.Value);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		[Route("stop-impersonate")]
		public ActionResult StopImpersonate()
		{
			_securityService.StopImpersonate();
			return RedirectToAction(nameof(ManageRoles));
		}

		#region Delegation

		[Authorize(Policy = "Trainers")]
		[Route("manage-delegate")]
		public async Task<IActionResult> ManageDelegates()
		{
			return View(new SecurityEmployeeDelegationViewModel());
		}

		[HttpPost]
		[Route("manage-delegate")]
		public async Task<IActionResult> AddDelegation(SecurityEmployeeDelegationViewModel model)
		{
			if (!ModelState.IsValid)
				return View("ManageDelegates", model);

			await _cachedSecurityService.AddDelegation(model);
			return RedirectToAction(nameof(ManageDelegates));
		}

		[HttpPost]
		[Route("delete-delegation")]
		public async Task<IActionResult> DeleteDelegation(int parentUpn, int delegateUpn)
		{
			await _cachedSecurityService.DeleteDelegation(parentUpn, delegateUpn);
			return Json(null);
		}

		#endregion Delegation
	}
}
