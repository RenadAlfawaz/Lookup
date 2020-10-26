using NIC.SBCPlatform.SharedModules.LookupManagement.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class LookupDetailedFetchingDto : EntityDto<Guid>
    {
        [MaxLength(500)]
        [Display(Name = Localization.LookupManagementResource.LookupName)]
        public string Name { get; set; }

        [MaxLength(500)]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.LookupEnglishName)]
        public string EnglishName { get; set; }

        [MaxLength(500)]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.LookupArabicName)]
        public string ArabicName { get; set; }

        [Display(Name = Localization.LookupManagementResource.LookupType)]
        public LookupTypeFetchingDto LookupType { get; set; }

        [Display(Name = Localization.LookupManagementResource.LookupIsActive)]
        public bool IsActive { get; set; }
    }
}
