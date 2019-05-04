using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Models;
using Tms.Web.ViewModels;

namespace Tms.Web.Interfaces
{
	public interface ICachedSecurityService
	{
		Task<IReadOnlyList<SecurityRoleDetail>> ListAllSecurityRoleDetails();

		Task<IReadOnlyList<string>> ListSecurityRoleDetailsOfUser(IEnumerable<int> upns);

		Task<IReadOnlyList<string>> ListSecurityRoleDetailsOfUser(int upn);

		Task RefreshSecurityRoles();

		Task SaveRoleDetails(SecurityUserRoleViewModel model);

		Task<int> CreateNewRole(SecurityUserRoleViewModel model);

		Task AddSecurityUserRole(int roleId, int upn);

		Task DeleteSecurityUserRole(int roleId, int upn);

		Task DeleteSecurityRole(int roleId);

		Task<SecurityUserRoleViewModel> GetSecurityRoleDetails(int id);

		Task<IReadOnlyList<SecurityEmployeeDelegation>> ListDelegations(int upn);

		Task<IReadOnlyList<int>> ListParentDelegates(int upn);

		Task AddDelegation(SecurityEmployeeDelegationViewModel model);

		Task DeleteDelegation(int parentUpn, int delegateUpn);

		Task RefreshDelegations(int parentUpn, int delegateUpn);
	}
}
