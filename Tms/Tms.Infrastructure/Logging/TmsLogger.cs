using Serilog;
using Serilog.Events;
using System;
using Tms.ApplicationCore.Interfaces;
using Tms.Infrastructure.Data;

namespace Tms.Infrastructure.Logging
{
	public class TmsLogger : ITmsLogger
	{
		private readonly ILogger _infoLogger;
		private readonly ILogger _errorLogger;
		private ITmsDapper _tmsDapper;
		private readonly ITmsConfiguration _tmsConfiguration;
		public readonly bool? CanInfoLog;
		public readonly bool? CanDbLog;

		public TmsLogger(ITmsDapper tmsDapper, ITmsConfiguration tmsConfiguration)
		{
			var currentDir = Environment.CurrentDirectory;
			_tmsDapper = tmsDapper;
			_tmsConfiguration = tmsConfiguration;
			CanInfoLog = _tmsConfiguration.Settings.Logging.CanInfoLog;
			CanDbLog = _tmsConfiguration.Settings.Logging.CanDbLog;

			_infoLogger = new LoggerConfiguration()
				.WriteTo.File(path: currentDir + "\\" + _tmsConfiguration.Settings.Logging.Info, rollingInterval: RollingInterval.Day)
					.CreateLogger();

			_errorLogger = new LoggerConfiguration()
				.WriteTo.File(path: currentDir + "\\" + _tmsConfiguration.Settings.Logging.Error, rollingInterval: RollingInterval.Day)
					.CreateLogger();
		}

		void ITmsLogger.LogInfo(string msg)
		{
			var infoToLog = new LogDetails()
			{
				Message = msg,
				Product = "TMS",
				Location = "",
				Hostname = Environment.MachineName,
				User = Environment.UserName
			};
			_infoLogger.Write(LogEventLevel.Information, "{@LogDetails}", infoToLog);
			//_tmsDapper.QueryNonQuery("", infoToLog);
		}

		void ITmsLogger.LogError(string msg)
		{
			var infoToLog = new LogDetails()
			{
				Message = msg,
				Product = "TMS",
				Location = "",
				Hostname = Environment.MachineName,
				User = Environment.UserName
			};
			if (infoToLog.Exception != null)
			{
				infoToLog.Message = GetMessageFromException(infoToLog.Exception);
			}
			_errorLogger.Write(LogEventLevel.Error, "{@LogDetails}", infoToLog);
			//_tmsDapper.QueryNonQuery("", infoToLog);
		}

		void ITmsLogger.LogError(LogDetails details)
		{
			if (details.Exception != null)
			{
				details.Message = GetMessageFromException(details.Exception);
			}
			_errorLogger.Write(LogEventLevel.Error, "{@LogDetails}", details);
			//_tmsDapper.QueryNonQuery("", details);
		}

		public static string GetMessageFromException(Exception ex)
		{
			if (ex.InnerException != null)
				return GetMessageFromException(ex.InnerException);

			return ex.Message;
		}
	}

	public interface ITmsLogger
	{
		void LogInfo(string msg);

		void LogError(string msg);

		void LogError(LogDetails details);
	}
}