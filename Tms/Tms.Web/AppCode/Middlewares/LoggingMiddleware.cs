using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tms.Infrastructure.Logging;

namespace Tms.Web.AppCode
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private ITmsLogger _tmsLogger;

		public LoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ITmsLogger tmsLogger)
		{
			_tmsLogger = tmsLogger;
			//TODO : Exact logging params have to  be decided.
			_tmsLogger.LogInfo("Request started at " + context.Request.Path);
			await _next(context);
			_tmsLogger.LogInfo(context.Request.Path + "Request completed");
		}
	}

	public static class LoggingMiddlewareExtension
	{
		public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<LoggingMiddleware>();
		}
	}
}
