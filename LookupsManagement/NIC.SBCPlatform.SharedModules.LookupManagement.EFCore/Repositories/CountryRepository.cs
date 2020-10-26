using Microsoft.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.EFCore;
using NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Repositories
{
    public class CountryRepository: EfCoreRepository<LookupManagementDbContext, Country, Guid>, ICountryRepository
    {
        public CountryRepository(IDbContextProvider<LookupManagementDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
        }

        public async Task<Country> FindAsync(Guid id)
        {
            return await base.GetQueryable().AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<QueryResult<Country>> FindAsync(QueryConstraints<Country> queryConstraints)
        {
             return await base.GetQueryable().AsNoTracking().ToResultAsync(queryConstraints);
        }

        public async Task<List<Country>> FindByIdsAsync(List<Guid> ids)
        {
            return await base.GetQueryable().AsNoTracking().Where(l => ids.Contains(l.Id) && l.IsActive == true).ToListAsync();
        }

        public async Task<Country> FirstOrDefaultAsync(QueryConstraints<Country> queryConstraints)
        {
            return await base.GetQueryable().AsNoTracking().ToFirstOrDefaultResultAsync(queryConstraints);
        }
    }
}
