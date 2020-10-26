using Microsoft.Extensions.DependencyInjection;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Repositories;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(LookupManagementDomainModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        //typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule)
       
        )]
    public class LookupManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            LookupManagementEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LookupManagementDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
                //Replaces IRepository<Book, Guid>
                //options.AddDefaultRepositories<LookupManagementDbContext>();
                options.AddRepository<Request, RequestRepository>();
                options.AddRepository<LookupType, LookupTypeRepository>();
                options.AddRepository<Country, CountryRepository>();
                options.AddRepository<Lookup, LookupRepository>();
                options.AddRepository<City, CityRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also LookupManagementMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
