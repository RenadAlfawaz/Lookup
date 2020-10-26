using NIC.SBCPlatform.SharedModules.LookupManagement.Localization;
using Volo.Abp.Application.Services;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    /* Inherit your application services from this class.
     */
    public abstract class LookupManagementAppService : ApplicationService
    {
        protected LookupManagementAppService()
        {
            LocalizationResource = typeof(LookupManagementResource);
        }
    }
}
