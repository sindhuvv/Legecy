using System.Collections.Generic;

namespace Tms.ApplicationCore.Models
{
	public class Settings
	{
		public Settings()
		{
			Security = new SecuritySetting();
			Caching = new CachingSetting();
			Logging = new LoggingSetting();
		}
		public SecuritySetting Security { get; private set; }
		public CachingSetting Caching { get; private set; }
		public LoggingSetting Logging { get; private set; }
	}

	public class LoggingSetting
	{
		public string Info { get; set; }
		public string Error { get; set; }
		public bool CanInfoLog { get; set; }
		public bool CanDbLog { get; set; }
	}

	public class SecuritySetting
	{
		public SecuritySetting()
		{
			AdminUPNs = new List<int>();
		}

		public List<int> AdminUPNs { get; set; }
	}
	public class CachingSetting
	{
		public int AbsoluteExpiration { get; set; }
	}
}
