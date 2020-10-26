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
using Volo.Abp.Users;
using Volo.Abp.Validation;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities
{  
    public class Lookup : BaseAuditedEntity<Guid>, ISoftDelete
    {

        #region Props

        [Required]
        [MaxLength(500)]
        [IsEnglish(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException)]
        [Display(Name = Localization.LookupManagementResource.LookupEnglishName)]
        public string EnglishName { get; private set; }

        [Required]
        [MaxLength(500)]
        [IsArabic(ErrorMessage = LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException)]
        [Display(Name = Localization.LookupManagementResource.LookupArabicName)]
        public string ArabicName { get; private set; }

        [Required]
       
        public Guid LookupTypeId { get; private set; }

        [Required]
        [Display(Name = Localization.LookupManagementResource.LookupIsActive)]
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Navigation

        [ForeignKey("LookupTypeId")]
        [Display(Name = Localization.LookupManagementResource.LookupType)]
        public virtual LookupType LookupType { get; set; }
       

        #endregion

        #region ctor

        public Lookup()
        {
        }

        public Lookup(string engName, string arName, Guid type, bool isActive)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            LookupTypeId = type;
            IsActive = isActive;


            CreationAudited();  
        }

        #endregion

        #region Behaviors

        public Lookup Update(string engName, string arName, Guid lookupType, bool isActive)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));
            LookupTypeId = lookupType;
            IsActive = isActive;

            ModificationAudited();

            return this;
        }

        public Lookup UpdateEnglishName(string engName)
        {
            EnglishName = engName ?? throw new ArgumentException("message", nameof(engName));

            ModificationAudited();

            return this;
        }

        public Lookup UpdateArabicName(string arName)
        {
            ArabicName = arName ?? throw new ArgumentException("message", nameof(arName));

            ModificationAudited();

            return this;
        }

        public Lookup UpdateType(Guid type)
        {
            LookupTypeId = type;


            ModificationAudited();

            return this;
        }

        public Lookup UpdateActivation(bool isActive)
        {
            IsActive = isActive;
             
            ModificationAudited();

            return this;
        } 

        #endregion
         
    }
}   

