using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;
using Tms.ApplicationCore.Models;
using Tms.Infrastructure.Data;
using Tms.Web.Interfaces;
using Tms.Web.ViewModels;

namespace Tms.Web.Services
{
	public class SecurityService : ISecurityService
	{
		private readonly ILogger _logger;
		private readonly ITmsDapper _dapper;
		private readonly IMapper _mapper;
		private IUnitOfWork _unitOfWork;
		private IHttpContextAccessor _httpContextAccessor;
		private readonly IIdentityService _identityService;

		public SecurityService(ILogger<SecurityService> logger, ITmsDapper dapper, IMapper mapper, IUnitOfWork unitOfWork
			, IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
		{
			_logger = logger;
			_dapper = dapper;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_identityService = identityService;
		}

		async Task<IReadOnlyList<SecurityRoleDetail>> ISecurityService.ListAllSecurityRoleDetails()
		{
			var data = await _dapper.QueryComplex((reader) =>
			{
				var roles = reader.Read<SecurityRoleDetail>().ToList();
				var userRoles = reader.Read<SecurityUserRole>().ToList();

				roles.ForEach(role =>
				{
					role.Upns = userRoles.Where(x => x.SecurityRoleId == role.Id).Select(x => x.Upn).ToList();
				});
				return roles;
			}, Sql.GetSecurityRoleDetails);

			return data;
		}

		async Task ISecurityService.SaveRoleDetails(SecurityUserRoleViewModel model)
		{
			if(model.CommaDelimitedSelectiedActions != null)
				model.Actions = GetPermissions(model.CommaDelimitedSelectiedActions);
			var dbModel = _mapper.Map<SecurityUserRoleViewModel, SecurityRole>(model);
			await _unitOfWork.SecurityRoleRepository.UpdateAsync(dbModel);
		}

		private List<Permissions> GetPermissions(string commaDelimitedSelectiedActions)
		{
			var list = new List<Permissions>();
			commaDelimitedSelectiedActions.Split(",").ToList().ForEach(x =>
			{
				list.Add((Permissions)(Convert.ToInt32(x)));
			});
			return list;
		}

		async Task<int> ISecurityService.CreateNewRole(SecurityUserRoleViewModel model)
		{
			model.Actions = GetPermissions(model.CommaDelimitedSelectiedActions);
			var dbModel = await _unitOfWork.SecurityRoleRepository.AddAsync(_mapper.Map<SecurityUserRoleViewModel, SecurityRole>(model));
			return dbModel.Id;			
		}

		async Task ISecurityService.AddSecurityUserRole(int roleId, int upn)
		{
			await _unitOfWork.SecurityUserRoleRepository.AddAsync(new SecurityUserRole()
			{
				SecurityRoleId = roleId,
				Upn = upn
			});
		}

		async Task ISecurityService.DeleteSecurityUserRole(int roleId, int upn)
		{
			string sql = @"DELETE FROM [dbo].[SecurityUserRole] WHERE SecurityRoleId = @id AND Upn = @upn";
			await _dapper.QueryNonQuery(sql, new { id = roleId, upn = upn });
		}

		async Task ISecurityService.DeleteSecurityRole(int roleId)
		{
			string sql = @"DELETE FROM [dbo].[SecurityUserRole] WHERE SecurityRoleId = @id
							DELETE FROM [dbo].[SecurityRole] WHERE Id = @id";
			await _dapper.QueryNonQuery(sql, new { id = roleId });
		}

		void ISecurityService.Impersonate(int upn)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Append(TmsConstants.Impersonation_Cookie_Name, upn.ToString());
		}

		void ISecurityService.StopImpersonate()
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Delete(TmsConstants.Impersonation_Cookie_Name);
		}

		bool ISecurityService.HasImpersonationToken()
		{
			if (_httpContextAccessor.HttpContext.Request.Cookies[TmsConstants.Impersonation_Cookie_Name] != null)
				return true;

			return false;
		}

		#region Delegation

		async Task<IReadOnlyList<SecurityEmployeeDelegation>> ISecurityService.ListDelegations(int upn)
		{
			return await _unitOfWork.SecurityEmployeeDelegationRepository.ListAsync(x => x.ParentUpn == upn);
		}

		async Task<IReadOnlyList<int>> ISecurityService.ListParentDelegates(int upn)
		{
			var dbModels = await _unitOfWork.SecurityEmployeeDelegationRepository.ListAsync(x => x.DelegateUpn == upn && x.EffectiveStartDate <= DateTime.Now.Date && x.EffectiveEndDate >= DateTime.Now.Date);
			return dbModels.Select(x => x.ParentUpn).ToList().AsReadOnly();
		}

		async Task<int> ISecurityService.AddDelegation(SecurityEmployeeDelegationViewModel model)
		{
			if (!model.ParentUpn.HasValue)
				model.ParentUpn = await _identityService.GetUserUpn();
			var dbModel = await _unitOfWork.SecurityEmployeeDelegationRepository.AddAsync(_mapper.Map<SecurityEmployeeDelegationViewModel, SecurityEmployeeDelegation>(model));
			return dbModel.Id;
		}

		async Task ISecurityService.DeleteDelegation(int parentUpn, int delegateUpn)
		{
			string sql = @"DELETE FROM [dbo].[Security_EmployeeDelegation] WHERE ParentUpn = @parentUpn AND DelegateUpn = @delegateUpn";
			await _dapper.QueryNonQuery(sql, new { parentUpn = parentUpn, delegateUpn  = delegateUpn });
		}

		#endregion
	}
}
