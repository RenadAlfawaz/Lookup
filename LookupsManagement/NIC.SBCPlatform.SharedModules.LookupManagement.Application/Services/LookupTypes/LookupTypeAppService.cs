using Microsoft.AspNetCore.Mvc;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{ 
    public class LookupTypeAppService : LookupManagementAppService, ILookupTypeAppService
    {
        #region private props

        private readonly ILookupTypeDomainService _lookupTypeDomainService;

        #endregion

        #region ctor

        public LookupTypeAppService(ILookupTypeDomainService lookupTypeDomainService)
        {
            _lookupTypeDomainService = lookupTypeDomainService;
        }

        #endregion

        #region Find

        [HttpGet]
       // [Route("api/app/LookupType/FindAll")]
        public async Task<List<LookupTypeFetchingDto>> GetAsync()
        {
            var lookupTypes = await _lookupTypeDomainService.FindAllAsync();
            return ObjectMapper.Map<List<LookupType>, List<LookupTypeFetchingDto>>(lookupTypes);
        }

        [HttpGet]
       // [Route("api/app/LookupType/FindById")]
        public async Task<LookupTypeFetchingDto> GetAsync(Guid id)
        {
            var lookupType = await _lookupTypeDomainService.FindByIdAsync(id);
            return ObjectMapper.Map<LookupType, LookupTypeFetchingDto>(lookupType);
        }

        #endregion

    }
}

