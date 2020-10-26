using NIC.SBCPlatform.SharedModules.LookupManagement.Attributes;
using NIC.SBCPlatform.SharedModules.LookupManagement.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities
{
    public class City : BaseAuditedEntity<Guid>, ISoftDelete
    { 
        #region Props

        [Required]
        [MaxLength(500)]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.CityEnglishName)]
        public string EnglishName { get; private set; }

        [Required]
        [MaxLength(500)]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.CityArabicName)]
        public string ArabicName { get; private set; }

        [Required]
        public Guid CountryId { get; private set; }

        [Required]
        [Display(Name = Localization.LookupManagementResource.CityIsActive)]
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Navigation

        [ForeignKey("CountryId")]
        [Display(Name = Localization.LookupManagementResource.Country)]
        public Country Country { get; private set; }
      

        #endregion

        #region ctor

        public City()
        {

        }

        public City(string engName, string arName, Guid countryId, bool isActive)

        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            CountryId = countryId;
            IsActive = isActive;

            CreationAudited();

        }

        #endregion

        #region Behaviors
        public City Update(string engName, string arName, Guid countryId, bool isActive)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            CountryId = countryId;
            IsActive = isActive;

            ModificationAudited();
            return this;
        }

        public City UpdateEnglishName(string engName)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ModificationAudited();
            return this;
        }

        public City UpdateArabicName(string arName)
        {
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            ModificationAudited();
            return this;
        }

        public City UpdateCountryId(Guid countryId)
        {
            CountryId = countryId;
            ModificationAudited();
            return this;
        }

        public City UpdateActivation(bool isActive)
        {
            IsActive = isActive;
            ModificationAudited();
            return this;
        }
        #endregion

    }
}
