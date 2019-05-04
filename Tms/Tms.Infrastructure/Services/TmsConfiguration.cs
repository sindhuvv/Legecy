using Microsoft.Extensions.Options;
using Tms.ApplicationCore.Interfaces;
using Tms.ApplicationCore.Models;

namespace Tms.Infrastructure.Services
{
	public class TmsConfiguration : ITmsConfiguration
	{
		IOptions<Settings> _settings;

		public TmsConfiguration(IOptions<Settings> settings)
		{
			_settings = settings;
		}

		public Settings Settings { get { return _settings.Value as Settings; } set { } }
	}
}
