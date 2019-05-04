using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Models;
using Tms.Web.ViewModels;

namespace Tms.Web.Interfaces
{
	public interface ISecurityService
	{
		Task<IReadOnlyList<SecurityRoleDetail>> ListAllSecurityRoleDetails();

		Task SaveRoleDetails(SecurityUserRoleViewModel model);

		Task<int> CreateNewRole(SecurityUserRoleViewModel model);

		Task AddSecurityUserRole(int roleId, int upn);

		Task DeleteSecurityUserRole(int roleId, int upn);

		Task DeleteSecurityRole(int roleId);

		void Impersonate(int upn);

		void StopImpersonate();

		bool HasImpersonationToken();

		Task<IReadOnlyList<SecurityEmployeeDelegation>> ListDelegations(int upn);

		Task<IReadOnlyList<int>> ListParentDelegates(int upn);

		Task<int> AddDelegation(SecurityEmployeeDelegationViewModel model);

		Task DeleteDelegation(int parentUpn, int delegateUpn);
	}
}
