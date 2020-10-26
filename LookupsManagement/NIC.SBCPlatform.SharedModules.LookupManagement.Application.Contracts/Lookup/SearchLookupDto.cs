using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Lookup
{
    public class SearchLookupDto 
    {
        [MaxLength(500)]
        [Display(Name = Localization.LookupManagementResource.LookupName)]
        public string Name { get; set; }
        [Display(Name = Localization.LookupManagementResource.LookupType)]
        public Guid? LookupType { get; set; }

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }
}
