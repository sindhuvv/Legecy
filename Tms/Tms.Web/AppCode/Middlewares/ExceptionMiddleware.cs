using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Tms.Infrastructure.Logging;

namespace Tms.Web.AppCode
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private ITmsLogger _tmsLogger;
		private readonly ExceptionHandlerOptions _options;

		public ExceptionMiddleware(
			RequestDelegate next,
		   IOptions<ExceptionHandlerOptions> options)
		{
			_next = next;
			_options = options.Value;
			if (_options.ExceptionHandler == null)
				_options.ExceptionHandler = _next;
		}

		public async Task Invoke(HttpContext context, ITmsLogger tmsLogger)
		{
			try
			{
				_tmsLogger = tmsLogger;
				await _next(context);
			}
			catch (Exception ex)
			{
				var infoToLog = new LogDetails()
				{
					Message = ex.Message,
					Product = "TMS",
					Location = context.Request.Path,
					Hostname = Environment.MachineName,
					User = Environment.UserName,
					Exception = ex
				};
				_tmsLogger.LogError(infoToLog);

				PathString originalPath = context.Request.Path;

				context.Request.Path = "/Home/Error";

				context.Response.Clear();
				var exceptionHandlerFeature = new ExceptionHandlerFeature()
				{
					Error = ex,
					Path = originalPath.Value,
				};

				context.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);
				context.Features.Set<IExceptionHandlerPathFeature>(exceptionHandlerFeature);
				context.Response.StatusCode = 500;

				await _options.ExceptionHandler(context);

				return;
			}
		}
	}

	public static class ExceptionMiddlewareExtension
	{
		public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
