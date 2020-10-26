using NIC.SBCPlatform.SharedModules.LookupManagement.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class CountryUpdateDto : EntityDto<Guid>
    {
        [MaxLength(500)]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.CountryEnglishName)]
        public string EnglishName { get; set; }

        [MaxLength(500)]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.CountryArabicName)]
        public string ArabicName { get; set; }

        [Display(Name = Localization.LookupManagementResource.CountryIsActive)]
        public bool IsActive { get; set; }
    }
}
