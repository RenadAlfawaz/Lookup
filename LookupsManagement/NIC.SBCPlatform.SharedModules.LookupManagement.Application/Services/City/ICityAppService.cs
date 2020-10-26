using NIC.SBCPlatform.SharedModules.LookupManagement.City;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{
    public interface ICityAppService
    {
        Task CreateAsync(CityCreationDto cityDto);
        Task UpdateAsync(CityUpdateDto cityDto);
        Task UpdateActivationStatusAsync(CityUpdateActivationDto cityDto);
        Task DeleteAsync(Guid id); 
        Task<CityDetailedFetchingDto> GetAsync(Guid id);
        Task<QueryResult<CityDetailedFetchingDto>> GetListAsync(CitySearchCriteria criteria);
        Task<List<CityFetchingDto>> GetListByIdsAsync(List<Guid> ids);
        Task<QueryResult<CityFetchingDto>> FindAsync(SearchCityDto criteria);
        Task<List<CityFetchingDto>> GetListAsync(Guid country);
    }
}
