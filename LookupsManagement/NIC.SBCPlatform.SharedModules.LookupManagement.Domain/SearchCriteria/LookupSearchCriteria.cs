using NIC.SBCPlatform.SharedModules.SharedInterfaces.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria
{
    public class LookupSearchCriteria : SearchCriteria
    {
        public Guid? Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string ArabicName { get; set; }

        [MaxLength(500)]
        public string EnglishName { get; set; }

        public Guid? LookupType { get; set; }

        public bool? IsActive { get; set; }

        public bool IncludeLookupType { get; set; }

        public LookupSearchCriteria() : base()
        {
        }

    }
}
