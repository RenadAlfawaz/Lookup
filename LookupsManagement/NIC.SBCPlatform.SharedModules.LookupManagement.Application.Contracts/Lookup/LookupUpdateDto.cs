using NIC.SBCPlatform.SharedModules.LookupManagement.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class LookupUpdateDto : EntityDto<Guid>
    {
        [MaxLength(500)]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.LookupEnglishName)]
        public string EnglishName { get; set; }

        [MaxLength(500)]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.LookupArabicName)]
        public string ArabicName { get; set; }

        [Display(Name = Localization.LookupManagementResource.LookupType)]
        public Guid LookupType { get; set; }

        [Display(Name = Localization.LookupManagementResource.LookupIsActive)]
        public bool IsActive { get; set; }
    }
}
