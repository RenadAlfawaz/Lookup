using NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace NIC.SBCPlatform.SharedModules.EntityFrameworkCore
{
    [DependsOn(
        typeof(LookupManagementEntityFrameworkCoreModule)
        )]
    public class EntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MigrationsDbContext>();
        }
    }
}
