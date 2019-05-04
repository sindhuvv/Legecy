using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Interfaces;
using Tms.Web.Interfaces;

namespace Tms.Web.Services
{
	public class SecurityUserClaims : IClaimsTransformation
	{
		private readonly ICachedSecurityService _cachedSecurityService;
		private readonly ILogger _logger;
		private readonly ILeadershipService _leadershipService;
		private readonly IIdentityService _identityService;

		public SecurityUserClaims(ICachedSecurityService cachedSecurityService, ILogger<SecurityUserClaims> logger, 
			ILeadershipService leadershipService, IIdentityService identityService)
		{
			_cachedSecurityService = cachedSecurityService;
			_logger = logger;
			_leadershipService = leadershipService;
			_identityService = identityService;
		}

		public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			_logger.LogDebug("TransformAsync Init");

			var upn = await _identityService.GetUserUpn();
			var identity = (ClaimsIdentity)principal.Identity;

			// copy existing windows auth 
			var claimsIdentity = new ClaimsIdentity(
				identity.Claims,
				identity.AuthenticationType,
				identity.NameClaimType,
				identity.RoleClaimType);

			var res = await _cachedSecurityService.ListSecurityRoleDetailsOfUser(upn);
			if (res.Any())
			{
				res.ToList().ForEach(p =>
				{
					claimsIdentity.AddClaim(new Claim(type: TmsConstants.RoleClaimType, value: p));
				});				
			}

			var parentDelegates = await _cachedSecurityService.ListParentDelegates(upn);
			res = await _cachedSecurityService.ListSecurityRoleDetailsOfUser(parentDelegates);
			if (res.Any())
			{
				res.ToList().ForEach(p =>
				{
					claimsIdentity.AddClaim(new Claim(type: TmsConstants.RoleClaimType, value: p, valueType: TmsConstants.Delegate));
				});
			}

			_logger.LogDebug("Added countOfClaims:" + res.Count().ToString());
			_logger.LogDebug("TransformAsync complete, CountOfClaims:" + claimsIdentity.Claims.Count());

			return new ClaimsPrincipal(claimsIdentity);
		}
	}
}
