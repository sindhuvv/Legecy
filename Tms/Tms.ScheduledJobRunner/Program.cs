using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tms.ScheduledJobRunner
{
	public class Program
	{
		static void Main(string[] args)
		{
			var isService = !(Debugger.IsAttached || args.Contains("--console"));

			if (isService)
			{
				var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
				var pathToContentRoot = Path.GetDirectoryName(pathToExe);
				Directory.SetCurrentDirectory(pathToContentRoot);
			}

			var builder = CreateWebHostBuilder(
				args.Where(arg => arg != "--console").ToArray());

			var host = builder.Build();

			if (isService)
			{
				host.RunAsCustomService();
			}
			else
			{
				host.RunAsync().Wait();
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseHttpSys(options =>
				{
					options.Authentication.Schemes =
						AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
					options.Authentication.AllowAnonymous = false;
					options.UrlPrefixes.Add("http://localhost:9000");
				});
	}
}
