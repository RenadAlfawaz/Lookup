using NIC.SBCPlatform.SharedModules.SharedInterfaces.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria
{
    public class SearchCriteria
    {
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public SortDirection SortDirection { get; set; }

        public SearchCriteria()
        {
            PageNumber = 1;
            PageSize = 10;
            SortDirection = SortDirection.Descending;
        }
    }
}
