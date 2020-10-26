using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class CityFetchingDto : EntityDto<Guid>
    {
        [MaxLength(500)]
        [Display(Name = Localization.LookupManagementResource.CityName)]
        public string Name { get; set; }

    }
}
