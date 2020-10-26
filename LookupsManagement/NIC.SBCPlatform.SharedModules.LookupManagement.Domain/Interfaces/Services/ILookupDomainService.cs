using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public interface ILookupDomainService
    {
        Task CreateAsync(string engName, string arName, Guid type, bool isActive);
        Task UpdateAsync(Guid id, string engName, string arName, Guid type, bool isActive);
        Task UpdateActivationStatusAsync(Guid id, bool isActive);
        Task DeleteAsync(Guid id); 
        Task<QueryResult<Lookup>> FindAsync(LookupSearchCriteria criteria);
        Task<Lookup> FindByIdAsync(Guid id);
        Task<List<Lookup>> FindByTypeAsync(Guid type);
        Task<List<Lookup>> FindByTypesAsync(Guid type);
        Task<List<Lookup>> FindByIdsAsync(List<Guid> ids); 

    }
}
