using NIC.SBCPlatform.SharedModules.SharedInterfaces.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria
{
    public class CitySearchCriteria : SearchCriteria
    {
        public Guid? Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string ArabicName { get; set; }

        [MaxLength(500)]
        public string EnglishName { get; set; }

        public Guid? Country { get; set; }

        public bool? IsActive { get; set; }

        [JsonIgnore]
        public bool IncludeCountry { get; set; }

        public CitySearchCriteria() : base()
        { 
        }

    }
}
