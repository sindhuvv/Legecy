using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EmailJobRunner
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			var ServicesToRun = new[] { new EmailJobService() };
			ServiceBase.Run(ServicesToRun);

			//EmailJobScheduler sc = new EmailJobScheduler();
			//sc.Start().Wait();			
					   
			//var clientHandler = new HttpClientHandler()
			//{
			//	Credentials = CredentialCache.DefaultNetworkCredentials
			//};
			//var client = new HttpClient(clientHandler);
			//client.BaseAddress = new Uri("http://localhost:64581/");
			//var HttpResponseMessage = client.GetAsync("api/Home/SendEmail").Result;
		}
	}
}
