using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.City
{
    public class SearchCityDto
    {
        [MaxLength(500)]
        [Display(Name = Localization.LookupManagementResource.CityName)]
        public string Name { get; set; }

        [Display(Name = Localization.LookupManagementResource.Country)]
        public Guid? Country { get; set; }

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }
}
