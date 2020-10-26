using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces
{
    public interface ILookupRepository: IBasicRepository<Lookup>
    {
        Task<Lookup> FindAsync(Guid id);
        Task<List<Lookup>> FindByTypeAsync(Guid type);
        Task<List<Lookup>> FindByIdsAsync(List<Guid> ids);
        Task<List<Lookup>> FindByTypesAsync(Guid type);
        Task<QueryResult<Lookup>> FindAsync(QueryConstraints<Lookup> constraints);
        Task<Lookup> FirstOrDefaultAsync(QueryConstraints<Lookup> constraints); 

    }
}
