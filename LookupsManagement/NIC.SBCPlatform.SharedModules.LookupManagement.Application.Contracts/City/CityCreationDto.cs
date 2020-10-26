using NIC.SBCPlatform.SharedModules.LookupManagement.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class CityCreationDto : EntityDto
    {
        [MaxLength(500)]
        [Required]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.CityEnglishName)]
        public string EnglishName { get; set; }

        [MaxLength(500)]
        [Required]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.CityArabicName)]
        public string ArabicName { get; set; }

        [Display(Name = Localization.LookupManagementResource.Country)]
        [Required]
        public Guid Country { get; set; }

        [Display(Name = Localization.LookupManagementResource.CityIsActive)]
        public bool IsActive { get; set; }

    }
}
