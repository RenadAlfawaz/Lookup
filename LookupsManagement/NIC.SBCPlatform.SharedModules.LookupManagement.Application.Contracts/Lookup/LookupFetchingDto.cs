using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class LookupFetchingDto : EntityDto<Guid>
    {
        [MaxLength(500)]
        [Display(Name = Localization.LookupManagementResource.LookupName)]
        public string  Name { get; set; }          
    }
}
