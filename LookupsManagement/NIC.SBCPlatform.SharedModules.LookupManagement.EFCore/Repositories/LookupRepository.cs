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
    public class LookupRepository : EfCoreRepository<LookupManagementDbContext, Lookup, Guid>, ILookupRepository
    {
        public LookupRepository(IDbContextProvider<LookupManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Lookup> FindAsync(Guid id)
        {
            return await base.GetQueryable().AsNoTracking().Include(a => a.LookupType).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Lookup>> FindByTypeAsync(Guid type)
        {
            return await base.GetQueryable().AsNoTracking().Include(a => a.LookupType).Where(l => l.LookupTypeId == type).ToListAsync();
        }

        public async Task<QueryResult<Lookup>> FindAsync(QueryConstraints<Lookup> queryConstraints)
        {
            return await base.GetQueryable().AsNoTracking().ToResultAsync(queryConstraints);
        }

        public async Task<List<Lookup>> FindByIdsAsync(List<Guid> ids)
        {
            return await base.GetQueryable().AsNoTracking().Where(l => ids.Contains(l.Id) && l.IsActive == true).ToListAsync();
        }

        public async Task<Lookup> FirstOrDefaultAsync(QueryConstraints<Lookup> constraints)
        {
            return await base.GetQueryable().AsNoTracking().ToFirstOrDefaultResultAsync(constraints);
        }

        public async Task<List<Lookup>> FindByTypesAsync(Guid type)
        {
            return await base.GetQueryable().AsNoTracking().Include(a => a.LookupType).Where(l => l.LookupTypeId == type && l.IsActive == true).ToListAsync();
        }
    }
}
