using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class CityUpdateActivationDto : EntityDto<Guid>
    {
        [Display(Name = Localization.LookupManagementResource.CityIsActive)]
        public bool IsActive { get; set; }
    }
}
