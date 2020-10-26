using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace NIC.SBCPlatform.SharedModules.Controllers
{

    public class HomeController : AbpController
    {
        public ActionResult Index()
        { 
            return Redirect("/swagger");
        }
    }
}
