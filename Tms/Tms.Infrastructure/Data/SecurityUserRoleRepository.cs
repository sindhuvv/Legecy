using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class SecurityUserRoleRepository : BaseRepository<SecurityUserRole>, ISecurityUserRoleRepository
	{
		public SecurityUserRoleRepository(TmsContext tmsContext, ITmsDapper dapper) : base(tmsContext, dapper)
		{

		}
	}
}
