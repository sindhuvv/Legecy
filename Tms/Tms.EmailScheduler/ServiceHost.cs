using DasMulli.Win32.ServiceUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tms.EmailScheduler
{
	public class ServiceHost : IWin32Service
	{
		public string ServiceName => throw new NotImplementedException();

		public void Start(string[] startupArguments, ServiceStoppedCallback serviceStoppedCallback)
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}
	}
}
