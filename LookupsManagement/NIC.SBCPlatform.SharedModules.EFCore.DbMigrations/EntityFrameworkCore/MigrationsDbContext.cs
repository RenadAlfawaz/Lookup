using NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.LookupManagement.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Users;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;

namespace NIC.SBCPlatform.SharedModules.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See LookupManagementDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class MigrationsDbContext : AbpDbContext<MigrationsDbContext>
    {

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigureAuditLogging();

            /* Configure your own tables/entities inside the ConfigureLookupManagement method */

            builder.ConfigureLookupManagement();
        }
    }
}