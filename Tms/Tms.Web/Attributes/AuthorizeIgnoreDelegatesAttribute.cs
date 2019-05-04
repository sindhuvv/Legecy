using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tms.ApplicationCore;

namespace Tms.Web.Attributes
{
	public class AuthorizeIgnoreDelegatesAttribute : TypeFilterAttribute
	{
		public readonly string Policy;

		public AuthorizeIgnoreDelegatesAttribute(string Policy) : base(typeof(AuthorizeIgnoreDelegatesFilter))
		{
			this.Policy = Policy;
			Arguments = new object[] { new Claim(TmsConstants.RoleClaimType, Policy) };
		}
	}

	public static class AuthorizeIgnoreDelegatesAttributeExtensions
	{
		public static async Task<bool> AssertAuthorized(this AuthorizeIgnoreDelegatesAttribute attribute, ClaimsPrincipal user)
		{
			var hasClaim = user.Claims.Any(c => c.Type == TmsConstants.RoleClaimType && c.Value == attribute.Policy && !c.ValueType.Equals(TmsConstants.Delegate));
			return hasClaim;
		}
	}

	public class AuthorizeIgnoreDelegatesFilter : IAuthorizationFilter
	{
		readonly Claim _claim;

		public AuthorizeIgnoreDelegatesFilter(Claim claim)
		{
			_claim = claim;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value && !c.ValueType.Equals(TmsConstants.Delegate));
			if (!hasClaim)
				context.Result = new ForbidResult();
		}
	}
}