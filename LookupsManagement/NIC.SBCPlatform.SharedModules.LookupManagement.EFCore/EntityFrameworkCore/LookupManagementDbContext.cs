using NIC.SBCPlatform.SharedModules.LookupManagement.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using NIC.SBCPlatform.SharedModules.LookupManagement.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See LookupManagementMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class LookupManagementDbContext : AbpDbContext<LookupManagementDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside LookupManagementDbContextModelCreatingExtensions.ConfigureLookupManagement
         */

        public LookupManagementDbContext(DbContextOptions<LookupManagementDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable("Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();

                /* Configure mappings for your additional properties
                 * Also see the LookupManagementEfCoreEntityExtensionMappings class
                 */
            });

            //builder.Entity<City>(b => b.HasOne(a => a.Country).WithMany().OnDelete(DeleteBehavior.Restrict));

            /* Configure your own tables/entities inside the ConfigureLookupManagement method */

            builder.ConfigureLookupManagement();
        }
    }
}
