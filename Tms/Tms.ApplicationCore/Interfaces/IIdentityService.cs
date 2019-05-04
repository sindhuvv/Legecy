using System.Threading.Tasks;

namespace Tms.ApplicationCore.Interfaces
{
	public interface IIdentityService
	{
		Task<int> GetUserUpn();
    }
}
