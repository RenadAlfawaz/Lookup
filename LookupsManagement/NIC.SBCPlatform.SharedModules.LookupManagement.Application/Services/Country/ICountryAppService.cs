using NIC.SBCPlatform.SharedModules.LookupManagement.Country;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{
    public interface ICountryAppService
    {
        Task CreateAsync(CountryCreationDto countryDto);
        Task UpdateAsync(CountryUpdateDto countryDto);
        Task UpdateActivationStatusAsync(CountryUpdateActivationDto countryDto);
        Task DeleteAsync(Guid id); 
        Task<CountryDetailedFetchingDto> GetAsync(Guid id);
        Task<QueryResult<CountryDetailedFetchingDto>> GetListAsync(CountrySearchCriteria criteria);
        Task<List<CountryFetchingDto>> GetListByIdsAsync(List<Guid> ids);
        Task<QueryResult<CountryFetchingDto>> FindAsync(SearchCountryDto criteria);

    }
}
