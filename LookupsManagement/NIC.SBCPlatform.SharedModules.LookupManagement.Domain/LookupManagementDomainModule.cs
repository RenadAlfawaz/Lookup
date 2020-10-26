using NIC.SBCPlatform.SharedModules.LookupManagement.ObjectExtending;
using Volo.Abp.AuditLogging;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    [DependsOn(
        typeof(LookupManagementDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpLocalizationModule)
        )]
    public class LookupManagementDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            LookupManagementDomainObjectExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
