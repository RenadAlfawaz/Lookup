using NIC.SBCPlatform.SharedModules.LookupManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Controllers
{
    /* Inherit your controllers from this class.
     */
    //[DontWrapResult(LogError = true, WrapOnError = false)]
    public abstract class LookupManagementController : AbpController
    {
        protected LookupManagementController()
        {
            LocalizationResource = typeof(LookupManagementResource);
        } 
    }
}