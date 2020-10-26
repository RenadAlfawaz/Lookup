using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public interface ILookupTypeDomainService
    {
        Task<List<LookupType>> FindAllAsync();
        Task<LookupType> FindByIdAsync(Guid id);
    }
}
