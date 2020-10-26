using Volo.Abp.Modularity;
namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    [DependsOn(
        typeof(LookupManagementApplicationContractsModule)
        )]
    public class LookupManagementHttpApiModule : AbpModule
    {

    }
}
