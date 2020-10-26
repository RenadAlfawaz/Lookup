using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public class LookupTypeDomainService: DomainService, ILookupTypeDomainService
    {
        private readonly ILookupTypeRepository _lookupTypeRepository;

        public LookupTypeDomainService(ILookupTypeRepository lookupTypeRepository)
        {
            _lookupTypeRepository = lookupTypeRepository;
        }

        public async Task<List<LookupType>> FindAllAsync()
        {
            var types = await _lookupTypeRepository.GetListAsync(true); 
            return types;
        }

        public async Task<LookupType> FindByIdAsync(Guid id)
        {
            var lookupType = await _lookupTypeRepository.FindAsync(id); 
            if (lookupType == null)
                throw new EntityNotFoundException(LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException);

            return lookupType;

        }
    }
}
