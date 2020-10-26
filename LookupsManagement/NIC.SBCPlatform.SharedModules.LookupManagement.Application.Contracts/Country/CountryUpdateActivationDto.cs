using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class CountryUpdateActivationDto: EntityDto<Guid>
    {
        [Display(Name = Localization.LookupManagementResource.CountryIsActive)]
        public bool IsActive { get; set; }
    }
}
