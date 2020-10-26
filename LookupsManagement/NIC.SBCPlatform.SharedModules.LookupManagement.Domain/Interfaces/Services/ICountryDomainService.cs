using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public interface ICountryDomainService
    {
        Task CreateAsync(string englishName, string arabicName, bool isActive);
        Task UpdateAsync(Guid id, string englishName, string arabicName, bool isActive);
        Task UpdateActivationStatusAsync(Guid id, bool isActive);
        Task DeleteAsync(Guid id);
        Task<QueryResult<Country>> FindAsync(CountrySearchCriteria criteria); 
        Task<Country> FindByIdAsync(Guid id);
        Task<List<Country>> FindByIdsAsync(List<Guid> countriesIds); 

    }
}
