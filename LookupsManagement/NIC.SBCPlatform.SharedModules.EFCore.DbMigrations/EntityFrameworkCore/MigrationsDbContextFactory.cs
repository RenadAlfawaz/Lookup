using System.IO;
using NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NIC.SBCPlatform.SharedModules.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<MigrationsDbContext>
    {
        public MigrationsDbContext CreateDbContext(string[] args)
        {
            LookupManagementEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new MigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
