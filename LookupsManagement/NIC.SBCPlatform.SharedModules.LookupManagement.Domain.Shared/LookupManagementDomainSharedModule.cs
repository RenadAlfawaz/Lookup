using NIC.SBCPlatform.SharedModules.LookupManagement.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    //[DependsOn(
    //    //typeof(AbpAuditLoggingDomainSharedModule)
    //    //,
    //    //typeof(AbpVirtualFileSystemModule)
    //    )]
    [DependsOn(typeof(AbpAuditLoggingDomainSharedModule))] 
    [DependsOn(typeof(AbpLocalizationModule))]
    public class LookupManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<LookupManagementDomainSharedModule>("NIC.SBCPlatform.SharedModules.LookupManagement");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<LookupManagementResource>("en") 
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/LookupManagement");
                
                options.DefaultResourceType = typeof(LookupManagementResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {  
                options.MapCodeNamespace("SharedModules.LookupManagement", typeof(LookupManagementResource)); 
            });
        }
    }
}
