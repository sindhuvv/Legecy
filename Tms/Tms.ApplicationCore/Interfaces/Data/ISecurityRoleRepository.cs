using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface ISecurityRoleRepository : IRepository<SecurityRole>, IAsyncRepository<SecurityRole>
	{

	}
}
