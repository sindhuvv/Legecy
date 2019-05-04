using System.Collections.Generic;

namespace Tms.Web.Interfaces
{
	public interface ILeadershipService
	{
		IReadOnlyList<IBuiltInRole> GetBuiltInRoles();
	}
}
