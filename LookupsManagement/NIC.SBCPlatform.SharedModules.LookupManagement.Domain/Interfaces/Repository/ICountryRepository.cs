using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces
{
    public interface ICountryRepository : IBasicRepository<Country>
    {
        Task<Country> FindAsync(Guid id);
        Task<List<Country>> FindByIdsAsync(List<Guid> countryIds); 
        Task<QueryResult<Country>> FindAsync(QueryConstraints<Country> constraints);
        Task<Country> FirstOrDefaultAsync(QueryConstraints<Country> constraints);
    }
}
