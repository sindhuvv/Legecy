using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Tms.ApplicationCore;
using Tms.ApplicationCore.Interfaces;
using Tms.ApplicationCore.Models;
using Tms.Infrastructure.Caching;
using Tms.Infrastructure.Data;
using Tms.Infrastructure.Export;
using Tms.Infrastructure.Logging;
using Tms.Infrastructure.Services;
using Tms.Web.Helpers;
using Tms.Web.Interfaces;
using Tms.Web.Models;
using Tms.Web.Services;
using Tms.Web.Services.Api;

namespace Tms.Web.AppCode
{
	public static class ServiceConfiguration
	{
		public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				//ToDo: Also consider options.MimeTypes later				
			});

			services.AddHttpContextAccessor();

			services.AddDbContext<TmsContext>(
				options => options.UseSqlServer(configuration.GetConnectionString(TmsConstants.ConnectionStringName)));

			services.AddDistributedMemoryCache();
			services.AddAutoMapper();

			services.Configure<Settings>(configuration);

			services.AddSingleton<ITmsConfiguration, TmsConfiguration>();

			services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
			services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

			services.AddScoped<ITrainingService, TrainingService>();
			services.AddScoped<ICachedTrainingService, CachedTrainingService>();
			services.AddScoped<ITrainingRepository, TrainingRepository>();
			services.AddScoped<IUtbService, UtbService>();
			services.AddScoped<ITrainingSessionRepository, TrainingSessionRepository>();
			
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ITmsDapper, TmsDapper>();
			services.AddScoped<ITmsCache, TmsCache>();
			services.AddScoped<ITmsLogger, TmsLogger>();
			services.AddScoped<IPdfGenerator, PdfGenerator>();
			services.AddScoped<IScriptHelper, ScriptHelper>();

			services.AddScoped<IOfficeDocumentGenerator, OfficeDocumentGenerator>();
			services.AddScoped<IWorksheetDataBinder, WorksheetDataBinder>();
			services.AddHttpClient("tmsClient", c =>
			{
				c.BaseAddress = new Uri("http://localhost:64581/api/");//TBD: Need to avoid magic string/make it configurable, this avoids setting Base address in each API call, 
			}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
			{
				Credentials = CredentialCache.DefaultNetworkCredentials
			});
			services.AddScoped<ITmsClient, TmsClient>();

			#region Security
			services.AddScoped<IIdentityService, IdentityService>();

			services.AddScoped<ICachedSecurityService, CachedSecurityService>();

			services.AddScoped<ISecurityService, SecurityService>();

			services.AddScoped<ISecurityUserRoleRepository, SecurityUserRoleRepository>();

			services.AddScoped<ISecurityRoleRepository, SecurityRoleRepository>();

			services.AddTransient<IClaimsTransformation, SecurityUserClaims>();

			services.AddTransient<ILeadershipService, LeadershipService>();

			services.AddTransient<ISecurityEmployeeDelegationRepository, SecurityEmployeeDelegationRepository>();

			services.AddAuthentication(IISDefaults.AuthenticationScheme);

			var sp = services.BuildServiceProvider();
			var roleRepository = sp.GetService<ISecurityRoleRepository>();
			services.AddAuthorization(options =>
			{
				var roles = roleRepository.ListAll().Select(x => x.RoleName).ToList();
				roles.ForEach(x =>
				{
					options.AddPolicy(x, policy => policy.RequireClaim(TmsConstants.RoleClaimType, x));
				});

				options.AddPolicy(
					"Trainers&RosterAdmin",
					policy => policy.RequireClaim(TmsConstants.RoleClaimType, "Trainers", "RosterAdmin"));
			});

			#endregion

			#region Email

			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IEmailQueueRepository, EmailQueueRepository>();
			services.AddScoped<IEmailAttachmentRepository, EmailAttachmentRepository>();
			services.AddScoped<IEmailLogRepository, EmailLogRepository>();
			services.AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

			#endregion
		}
	}
}
