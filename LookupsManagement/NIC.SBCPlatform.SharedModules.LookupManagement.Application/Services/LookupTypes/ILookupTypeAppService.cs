using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{
    public interface ILookupTypeAppService
    {
        Task<List<LookupTypeFetchingDto>> GetAsync();
        Task<LookupTypeFetchingDto> GetAsync(Guid id);
    }
}
