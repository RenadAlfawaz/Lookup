using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Country
{
    public class SearchCountryDto
    {
        [MaxLength(500)]
        [Display(Name = Localization.LookupManagementResource.CountryName)]
        public string Name { get; set; }

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }
}
       
