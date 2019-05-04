using System.Linq;
using System.Text;
using Tms.Web.Helpers;

namespace Tms.Web.Extensions
{
	public static class HttpContextExtensions
	{
		public static string GetUserClaims(this Microsoft.AspNetCore.Http.HttpContext httpContext)
		{
			StringBuilder resp = new StringBuilder();
			int permflag = 0;
			var claims = httpContext.User.Claims.Where(p => p.Type == "PermFlags").ToList();
			claims.ForEach
			(
				p =>
				{
					resp.AppendLine(p.ToString());
					permflag = int.Parse(p.Value);
					SecurityHelper.GetPermFlags(permflag).ForEach(
							p1 => resp.AppendLine(p1.ToString())
					);
				}
			);
			return resp.ToString();
		}
	}
}
