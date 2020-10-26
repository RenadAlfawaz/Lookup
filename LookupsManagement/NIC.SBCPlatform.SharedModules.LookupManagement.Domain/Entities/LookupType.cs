using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities
{
    public class LookupType : Entity<Guid>
    {
        #region Props

        [Required]
        [MaxLength(500)]
        public string EnglishName { get; private set; }

        [Required]
        [MaxLength(500)]
        public string ArabicName { get; private set; }

        #endregion

        #region Ctor

        public LookupType()
        {

        }

        #endregion
    }
}
