using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Models;
using Tms.Infrastructure.Caching;
using Tms.Web.Interfaces;
using Tms.Web.ViewModels;

namespace Tms.Web.Services
{
	public class CachedSecurityService : ICachedSecurityService
	{
		private readonly ICachedSecurityService _this;
		private readonly ISecurityService _securityService;
		private readonly IMapper _mapper;
		private readonly ITmsCache _cache;
		private readonly ILeadershipService _leadershipService;

		#region cache Keys
		private const string ListAllRoles_CacheKey = "list-all-roles";
		private const string ListDelegations_CacheKey = "list-delegations";
		private const string ListParentDelegates_CacheKey = "list-parent-delegates";
		#endregion

		public CachedSecurityService(ISecurityService securityService, ITmsCache cache, IMapper mapper, ILeadershipService leadershipService)
		{
			_this = this;
			_securityService = securityService;
			_cache = cache;
			_mapper = mapper;
			_leadershipService = leadershipService;
		}

		async Task<IReadOnlyList<SecurityRoleDetail>> ICachedSecurityService.ListAllSecurityRoleDetails()
		{
			var cache_key = ListAllRoles_CacheKey;
			var details = await _cache.GetAsync<IReadOnlyList<SecurityRoleDetail>>(cache_key);
			if (details == null)
			{
				details = await _securityService.ListAllSecurityRoleDetails();
				await _cache.SetAsync(cache_key, details);
			}
			return details;
		}

		async Task<IReadOnlyList<string>> ICachedSecurityService.ListSecurityRoleDetailsOfUser(IEnumerable<int> upns)
		{
			var allRoles = await ((ICachedSecurityService)this).ListAllSecurityRoleDetails();
			var res = allRoles.Where(role =>
			{
				foreach (var upn in upns)
				{
					if (role.Upns.Any(x => x == upn))
						return true;
				}				
				return false;
			}).Select(x => x.RoleName).ToList();
			res.AddRange(_leadershipService.GetBuiltInRoles().Where(x => {
				var isMemeber = false;
				foreach (var user in upns)
				{
					if (x.IsMember(user, this))
						isMemeber = true;					
				}
				return isMemeber;
			}).Select(x => x.RoleName));
			return res.AsReadOnly();
		}

		async Task<IReadOnlyList<string>> ICachedSecurityService.ListSecurityRoleDetailsOfUser(int upn)
		{
			return await ((ICachedSecurityService)this).ListSecurityRoleDetailsOfUser(new List<int>() { upn });
		}

		async Task ICachedSecurityService.RefreshSecurityRoles()
		{
			var cache_key = ListAllRoles_CacheKey;
			await _cache.RemoveAsync(cache_key);
		}

		async Task ICachedSecurityService.SaveRoleDetails(SecurityUserRoleViewModel model)
		{
			await _securityService.SaveRoleDetails(model);
			await _this.RefreshSecurityRoles();
		}

		async Task<int> ICachedSecurityService.CreateNewRole(SecurityUserRoleViewModel model)
		{
			var id = await _securityService.CreateNewRole(model);
			await _this.RefreshSecurityRoles();
			return id;
		}

		async Task ICachedSecurityService.AddSecurityUserRole(int roleId, int upn)
		{
			await _securityService.AddSecurityUserRole(roleId, upn);
			await _this.RefreshSecurityRoles();
		}

		async Task ICachedSecurityService.DeleteSecurityUserRole(int roleId, int upn)
		{
			await _securityService.DeleteSecurityUserRole(roleId, upn);
			await _this.RefreshSecurityRoles();
		}

		async Task ICachedSecurityService.DeleteSecurityRole(int roleId)
		{
			await _securityService.DeleteSecurityRole(roleId);
			await _this.RefreshSecurityRoles();
		}

		async Task<SecurityUserRoleViewModel> ICachedSecurityService.GetSecurityRoleDetails(int id)
		{
			var roles = await _this.ListAllSecurityRoleDetails();
			var roleDetails = roles.Single(x => x.Id == id);
			return _mapper.Map<SecurityRoleDetail, SecurityUserRoleViewModel>(roleDetails); ;
		}

		#region Delegation
		
		async Task<IReadOnlyList<SecurityEmployeeDelegation>> ICachedSecurityService.ListDelegations(int upn)
		{
			var cache_key = ListDelegations_CacheKey + upn;
			var details = await _cache.GetAsync<IReadOnlyList<SecurityEmployeeDelegation>>(cache_key);
			if (details == null)
			{
				details = await _securityService.ListDelegations(upn);
				await _cache.SetAsync(cache_key, details);
			}
			return details;
		}

		async Task ICachedSecurityService.RefreshDelegations(int parentUpn, int delegateUpn)
		{
			var cache_key = ListDelegations_CacheKey + parentUpn;
			await _cache.RemoveAsync(cache_key);
			cache_key = ListDelegations_CacheKey + delegateUpn;
			await _cache.RemoveAsync(cache_key);

			cache_key = ListParentDelegates_CacheKey + parentUpn;
			await _cache.RemoveAsync(cache_key);
			cache_key = ListParentDelegates_CacheKey + delegateUpn;
			await _cache.RemoveAsync(cache_key);
		}

		async Task<IReadOnlyList<int>> ICachedSecurityService.ListParentDelegates(int upn)
		{
			var cache_key = ListParentDelegates_CacheKey + upn;
			var details = await _cache.GetAsync<IReadOnlyList<int>>(cache_key);
			if (details == null)
			{
				details = await _securityService.ListParentDelegates(upn);
				await _cache.SetAsync(cache_key, details);
			}
			return details;
		}

		async Task ICachedSecurityService.AddDelegation(SecurityEmployeeDelegationViewModel model)
		{
			await _securityService.AddDelegation(model);
			await _this.RefreshDelegations(model.ParentUpn.Value, model.DelegateUpn.Value);
		}

		async Task ICachedSecurityService.DeleteDelegation(int parentUpn, int delegateUpn)
		{
			await _securityService.DeleteDelegation(parentUpn, delegateUpn);
			await _this.RefreshDelegations(parentUpn, delegateUpn);
		}

		#endregion
	}
}
