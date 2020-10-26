using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class LookupTypeFetchingDto : EntityDto<Guid>
    {
        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string EnglishName { get; set; }

        [MaxLength(500)]
        public string ArabicName { get; set; }
    }
}
