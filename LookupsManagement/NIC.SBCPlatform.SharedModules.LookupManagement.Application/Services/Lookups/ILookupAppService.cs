using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.LookupManagement.Lookup;
using NIC.SBCPlatform.SharedModules.LookupManagement.Repositories;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{
    public interface ILookupAppService
    {
        Task CreateAsync(LookupCreationDto lookupDto);
        Task UpdateAsync(LookupUpdateDto lookupDto);
        Task UpdateActivationStatusAsync(LookupUpdateActivationDto lookupDto);
        Task DeleteAsync(Guid id); 
        Task<LookupDetailedFetchingDto> GetAsync(Guid id);
        //Task<List<LookupFetchingDto>> FindByTypeAsync(Guid type);
        Task<List<LookupFetchingDto>> GetListByIdsAsync(List<Guid> ids);
        Task<List<LookupFetchingDto>> GetListAsync(Guid type);
        Task<QueryResult<LookupDetailedFetchingDto>> GetListAsync(LookupSearchCriteria criteria);
        Task<QueryResult<LookupFetchingDto>> FindAsync(SearchLookupDto criteria);
         
    }
    
}

