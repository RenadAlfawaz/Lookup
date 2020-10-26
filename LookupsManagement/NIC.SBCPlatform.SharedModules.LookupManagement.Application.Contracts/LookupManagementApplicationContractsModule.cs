using NIC.SBCPlatform.SharedModules.LookupManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    [DependsOn(
        typeof(LookupManagementDomainSharedModule),
        typeof(AbpObjectExtendingModule) ,
        typeof(AbpLocalizationModule)
    )]
   
    public class LookupManagementApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            LookupManagementDtoExtensions.Configure();
        }
        
    }
}
