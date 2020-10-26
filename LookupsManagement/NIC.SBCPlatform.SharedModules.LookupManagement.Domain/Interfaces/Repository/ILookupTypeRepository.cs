using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces
{
    public interface ILookupTypeRepository: IBasicRepository<LookupType>
    {
        Task<LookupType> FindAsync(Guid id);
    }
}
