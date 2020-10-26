using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces
{
    public interface ICityDomainService
    {
        Task CreateAsync(string englishName, string arabicName, Guid countryId, bool isActive);
        Task UpdateAsync(Guid id, string englishName, string arabicName, Guid countryId, bool isActive);
        Task UpdateActivationStatusAsync(Guid id, bool isActive);
        Task DeleteAsync(Guid id);
        Task<QueryResult<City>> FindAsync(CitySearchCriteria criteria);
        Task<City> FindByIdAsync(Guid id);
        Task<List<City>> FindByCountryAsync(Guid countryId);
        Task<List<City>> FindByIdsAsync(List<Guid> citiesIds);
    }
}
