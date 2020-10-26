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
    public interface  ICityRepository : IBasicRepository<City>
    {
        Task<City> FindAsync(Guid id);
        Task<QueryResult<City>> FindAsync(QueryConstraints<City> constraints);
        Task<List<City>> FindByIdsAsync(List<Guid> cityIds); 
        Task<List<City>> FindByCountryAsync(Guid countryId);
        Task<City> FirstOrDefaultAsync(QueryConstraints<City> constraints);

    }
}
