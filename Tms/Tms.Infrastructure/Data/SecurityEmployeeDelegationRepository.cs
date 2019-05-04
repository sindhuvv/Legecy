using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class SecurityEmployeeDelegationRepository : BaseRepository<SecurityEmployeeDelegation>, ISecurityEmployeeDelegationRepository
	{
		public SecurityEmployeeDelegationRepository(TmsContext tmsContext, ITmsDapper dapper) : base(tmsContext, dapper)
		{

		}
	}
}
