using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tms.Web.Extensions
{
	public static class AuthorizeAttributeExtensions
	{
		public static async Task<bool> AssertAuthorized(this AuthorizeAttribute attribute, IAuthorizationService authService, ClaimsPrincipal user)
		{
			var authorize = await authService.AuthorizeAsync(user, attribute.Policy);
			if (authorize.Succeeded)
				return true;
			return false;
		}
	}
}