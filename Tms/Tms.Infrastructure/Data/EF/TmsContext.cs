using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class TmsContext : DbContext
	{
		private readonly IIdentityService _identityService;

		public TmsContext(DbContextOptions options, IIdentityService identityService)
		 : base(options)
		{
			_identityService = identityService;
		}

		public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateTrackingDetails();
			return await base.SaveChangesAsync(cancellationToken);
		}

		public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateTrackingDetails();
			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			UpdateTrackingDetails();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override int SaveChanges()
		{
			UpdateTrackingDetails();
			return base.SaveChanges();
		}

		private async Task UpdateTrackingDetails()
		{
			var entities = this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

			var upn = await _identityService.GetUserUpn();

			entities.ForEach(x =>
			{
				var now = DateTime.UtcNow;
				if (x.Entity is ILastUpdatedByTracking)
				{
					((ILastUpdatedByTracking)x.Entity).LastUpdatedBy = upn;
					((ILastUpdatedByTracking)x.Entity).LastUpdatedDateTime = now;
				}
				if (x.Entity is ICreatedByTracking)
				{
					if (((ICreatedByTracking)x.Entity).CreatedBy == default(int))
					{
						((ICreatedByTracking)x.Entity).CreatedBy = upn;
						((ICreatedByTracking)x.Entity).CreatedDateTime = now;
					}
				}
			});
		}
		public DbSet<EmailQueue> EmailQueue { get; set; }
		public DbSet<EmailAttachment> EmailAttachments { get; set; }
		public DbSet<EmailLog> EmailLogs { get; set; }
		public DbSet<Training> Trainings { get; set; }
		public DbSet<TrainingSession> TrainingSessions { get; set; }
		public DbSet<TrainingSessionRoster> TrainingSessionRoster { get; set; }
		public DbSet<SecurityUserRole> SecurityUserRoles { get; set; }
		public DbSet<SecurityRole> SecurityRoles { get; set; }
		public DbSet<SecurityEmployeeDelegation> SecurityEmployeeDelegations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Training>().ToTable("Training");
			modelBuilder.Entity<TrainingSession>().ToTable("TrainingSession");
			modelBuilder.Entity<TrainingSessionRoster>().ToTable("TrainingSessionRoster");
			modelBuilder.Entity<SecurityUserRole>().ToTable("SecurityUserRole");
			modelBuilder.Entity<SecurityRole>().ToTable("SecurityRole");
			modelBuilder.Entity<SecurityEmployeeDelegation>().ToTable("Security_EmployeeDelegation");
		}
	}
}