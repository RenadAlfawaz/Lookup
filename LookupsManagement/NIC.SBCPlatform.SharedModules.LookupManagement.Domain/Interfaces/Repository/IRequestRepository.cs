using NIC.SBCPlatform.SharedModules.LookupManagement.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;


namespace NIC.SBCPlatform.SharedModules.LookupManagement.Interfaces
{ 
    public interface IRequestRepository : IBasicRepository<Request>
    {
        Task<Request> FindAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default);
        Task<List<Request>> FindByNameAsync(string name, int pageIndex = 1, int pageSize = 10);
    }
}
