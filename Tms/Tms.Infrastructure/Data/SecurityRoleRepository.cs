using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class SecurityRoleRepository : BaseRepository<SecurityRole>, ISecurityRoleRepository
	{
		public SecurityRoleRepository(TmsContext tmsContext) : base(tmsContext)
		{

		}
	}
}
