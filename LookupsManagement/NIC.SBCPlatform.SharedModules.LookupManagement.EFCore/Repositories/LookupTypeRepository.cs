using Microsoft.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Repositories
{

    public class LookupTypeRepository : EfCoreRepository<LookupManagementDbContext, LookupType, Guid>, ILookupTypeRepository
    {
        public LookupTypeRepository(IDbContextProvider<LookupManagementDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<LookupType> FindAsync(Guid id)
        {
            return await base.GetQueryable().AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
