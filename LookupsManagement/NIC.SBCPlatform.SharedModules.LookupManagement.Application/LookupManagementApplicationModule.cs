using System.Reflection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.ExceptionHandling;
using System.Net;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    [DependsOn(
        //typeof(LookupManagementDomainModule),
        //typeof(LookupManagementApplicationContractsModule),
        //typeof(AbpCachingModule),
        //typeof(AbpBackgroundJobsHangfireModule),
        //typeof(AbpAutoMapperModule),
        //typeof(AbpVirtualFileSystemModule)

        typeof(LookupManagementDomainModule),
        typeof(LookupManagementApplicationContractsModule),
        typeof(AbpCachingModule),
        //typeof(AbpBackgroundJobsHangfireModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpLocalizationModule)


        )]

    public class LookupManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<LookupManagementApplicationModule>();
            }); 
        }
    }
}
