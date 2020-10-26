using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Volo.Abp.Domain.Entities.Auditing;
using NIC.SBCPlatform.SharedModules.LookupManagement.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using NIC.SBCPlatform.SharedModules.LookupManagement.Attributes;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities
{
    public class Country : BaseAuditedEntity<Guid>, ISoftDelete
    {
        #region Props

        [Required]
        [MaxLength(500)]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.CountryEnglishName)] 
        public string EnglishName { get; private set; }

        [Required]
        [MaxLength(500)]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.CountryArabicName)]
        public string ArabicName { get; private set; }

        [Required]
        [Display(Name = Localization.LookupManagementResource.CountryIsActive)]
        public bool IsActive { get; private set; }
        public bool IsDeleted { get ; set ;}

        #endregion

        #region Ctor

        public Country()
        {

        }

        public Country(string engName, string arName, bool isActive)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            IsActive = isActive;

            CreationAudited();
        }

        #endregion

        #region Behaviors

        public Country Update(string engName, string arName, bool isActive)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            IsActive = isActive;

            ModificationAudited();
            
            return this;
        }

        public Country UpdateEnglishName(string engName)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName)); 
            ModificationAudited();

            return this;
        }

        public Country UpdateArabicName(string arName)
        {
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            ModificationAudited();

            return this;
        }

        public Country UpdateActivation(bool isActive)
        {
            IsActive = isActive; 
            ModificationAudited();

            return this;
        }

        #endregion
  
    }
}
