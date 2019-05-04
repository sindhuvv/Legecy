using System.Security.Principal;

namespace Tms.ApplicationCore.Interfaces
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
