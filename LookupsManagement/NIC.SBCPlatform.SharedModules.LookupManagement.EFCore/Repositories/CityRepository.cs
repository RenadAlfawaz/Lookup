using Microsoft.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using NIC.SBCPlatform.SharedModules.LookupManagement.EFCore;
using System.Collections.Generic;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Repositories
{
    public class CityRepository : EfCoreRepository<LookupManagementDbContext, City, Guid>, ICityRepository
    {
        public CityRepository(IDbContextProvider<LookupManagementDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
          
        public async Task<City> FindAsync(Guid id)
        {
            return await base.GetQueryable().AsNoTracking().Include(a => a.Country).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<QueryResult<City>> FindAsync(QueryConstraints<City> queryConstraints)
        {
            return await base.GetQueryable().AsNoTracking().ToResultAsync(queryConstraints);
        }

        public async Task<List<City>> FindByCountryAsync(Guid country)
        {
            return await base.GetQueryable().AsNoTracking().Include(a => a.Country).Where(l => l.CountryId == country && l.IsActive == true).ToListAsync();
        }

        public async Task<List<City>> FindByIdsAsync(List<Guid> ids)
        {
            return await base.GetQueryable().AsNoTracking().Where(l => ids.Contains(l.Id) && l.IsActive == true).ToListAsync();
        }

        public async Task<City> FirstOrDefaultAsync(QueryConstraints<City> queryConstraints)
        {
            return await base.GetQueryable().AsNoTracking().ToFirstOrDefaultResultAsync(queryConstraints);
        }
    }
}
