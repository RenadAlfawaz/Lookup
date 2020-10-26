using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services;
using NIC.SBCPlatform.SharedModules.LookupManagement.EFCore;
using NIC.SBCPlatform.SharedModules.LookupManagement.Lookup;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;


namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{
    public class LookupAppService : ApplicationService, ILookupAppService
    {
        #region private methods

        private readonly ILookupDomainService _lookupDomainService;

        #endregion

        #region ctor

        public LookupAppService(ILookupDomainService lookupDomainService) : base()
        {
            _lookupDomainService = lookupDomainService;
        }

        #endregion

        #region Admin Services
        [Authorize("Admin")]
        [HttpPost]
       // [Route("api/app/admin/Lookup/Create")]
        public async Task CreateAsync(LookupCreationDto lookupDto)
        {
            await _lookupDomainService.CreateAsync(lookupDto.EnglishName, lookupDto.ArabicName, lookupDto.LookupType, lookupDto.IsActive);
        }
        [Authorize("Admin")]
        [HttpPut]
        //[Route("api/app/admin/Lookup/Update")]
        public async Task UpdateAsync(LookupUpdateDto lookupDto)
        {
            await _lookupDomainService.UpdateAsync(lookupDto.Id, lookupDto.EnglishName, lookupDto.ArabicName, lookupDto.LookupType, lookupDto.IsActive);
        }
        [Authorize("Admin")]
        [HttpPut]
       // [Route("api/app/admin/Lookup/UpdateActivationStatus")]
        public async Task UpdateActivationStatusAsync(LookupUpdateActivationDto lookupDto)
        {
            await _lookupDomainService.UpdateActivationStatusAsync(lookupDto.Id, lookupDto.IsActive);
        }
        [Authorize("Admin")]
        [HttpDelete]
        //[Route("api/app/admin/Lookup/Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _lookupDomainService.DeleteAsync(id);
        }
        [Authorize("Admin")]
        [HttpGet]
        //[Route("api/app/admin/Lookup/FindById")]
        public async Task<LookupDetailedFetchingDto> GetAsync(Guid id)
        {
            var lookup = await _lookupDomainService.FindByIdAsync(id); 
            return ObjectMapper.Map<Domain.Entities.Lookup, LookupDetailedFetchingDto>(lookup);
        }
        //[Authorize("Admin")]
        //[HttpGet]
        ////[Route("api/app/admin/Lookup/FindByType")]
        //public async Task<List<LookupFetchingDto>> FindByTypeAsync(Guid type)
        //{
        //    var lookups = await _lookupDomainService.FindByTypeAsync(type); 
        //    return ObjectMapper.Map<List<Domain.Entities.Lookup>, List<LookupFetchingDto>>(lookups);
        //}
        [Authorize("Admin")]
        [HttpGet]
       // [Route("api/app/admin/Lookup/Find")]
        public async Task<QueryResult<LookupDetailedFetchingDto>> GetListAsync(LookupSearchCriteria criteria)
        {
           
            criteria.IncludeLookupType = true;

            var lookups = await _lookupDomainService.FindAsync(criteria); 
            return lookups.MapToModel<Domain.Entities.Lookup, LookupDetailedFetchingDto>(ObjectMapper); 
        }

        #endregion

        #region End Users Services

        [HttpGet]
        //[Route("api/app/Lookup/FindByIds")]
        public async Task<List<LookupFetchingDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var lookups = await _lookupDomainService.FindByIdsAsync(ids);
            return ObjectMapper.Map<List<Domain.Entities.Lookup>, List<LookupFetchingDto>>(lookups);
        }
         
        [HttpGet]
        //[Route("api/app/Lookup/Find")]
        public async Task<QueryResult<LookupFetchingDto>> FindAsync(SearchLookupDto criteria)
        {
            var searchCriteria = ObjectMapper.Map<SearchLookupDto, LookupSearchCriteria>(criteria);
            searchCriteria.IsActive = true;
            searchCriteria.IncludeLookupType = true;

            var lookups = await _lookupDomainService.FindAsync(searchCriteria); 
            return lookups.MapToModel<Domain.Entities.Lookup, LookupFetchingDto>(ObjectMapper); 
        }

        [HttpGet]
        [Route("api/app/lookup/{lookuptypeid}/getlookups")]
        public async Task<List<LookupFetchingDto>> GetListAsync(Guid lookupTypeId)
        {
            var lookups = await _lookupDomainService.FindByTypesAsync(lookupTypeId);
            return ObjectMapper.Map<List<Domain.Entities.Lookup>, List<LookupFetchingDto>>(lookups);
        }

       
        #endregion
    }
}
