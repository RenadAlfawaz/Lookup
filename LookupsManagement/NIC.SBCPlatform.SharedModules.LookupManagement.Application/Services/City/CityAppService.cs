using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIC.SBCPlatform.SharedModules.LookupManagement.City;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.LookupManagement.EFCore;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{
    
    public class CityAppService : LookupManagementAppService, ICityAppService
    {
        #region private props

        private readonly ICityDomainService _cityDomainService;

        #endregion

        #region ctor

        public CityAppService(ICityDomainService cityDomainService)
        {
            _cityDomainService = cityDomainService;
        }

        #endregion

        #region Admin Services
        [Authorize("Admin")]
        [HttpPost]
        //[Route("api/app/admin/City/Create")]
        public async Task CreateAsync(CityCreationDto cityDto)
        {
            await _cityDomainService.CreateAsync(cityDto.EnglishName, cityDto.ArabicName,cityDto.Country, cityDto.IsActive);
        }

        [Authorize("Admin")]
        [HttpPut]
        //[Route("api/app/admin/City/Update")]
        public async Task UpdateAsync(CityUpdateDto cityDto)
        { 

            await _cityDomainService.UpdateAsync(cityDto.Id, cityDto.EnglishName, cityDto.ArabicName, cityDto.Country, cityDto.IsActive);
        }

        [Authorize("Admin")]
        [HttpPut]
       // [Route("api/app/admin/City/UpdateActivationStatus")]
        public async Task UpdateActivationStatusAsync(CityUpdateActivationDto cityDto)
        {
            await _cityDomainService.UpdateActivationStatusAsync(cityDto.Id, cityDto.IsActive);
        }

        [Authorize("Admin")]
        [HttpDelete]
        //[Route("api/app/admin/City/Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _cityDomainService.DeleteAsync(id);
        }
        
       
        [HttpGet]
        [Authorize("Admin")]
        //[Route("api/app/admin/city/getlistasync")]
        public async Task<QueryResult<CityDetailedFetchingDto>> GetListAsync(CitySearchCriteria criteria)
        {
            criteria.IncludeCountry = true;
            var cities = await _cityDomainService.FindAsync(criteria);
            return cities.MapToModel<Domain.Entities.City, CityDetailedFetchingDto>(ObjectMapper);
        }

        [HttpGet]
        [Authorize("Admin")]
        //[Route("api/app/City/FindById")]
        public async Task<CityDetailedFetchingDto> GetAsync(Guid id)
        {
            var userroles = CurrentUser.Roles;
            var userrClaims = CurrentUser.GetAllClaims();
            var city = await _cityDomainService.FindByIdAsync(id);
            return ObjectMapper.Map<Domain.Entities.City, CityDetailedFetchingDto>(city);
        }
         
        #endregion

        #region End Users Services

        [HttpGet]
        //[Route("api/app/City/FindByIds")]
        public async Task<List<CityFetchingDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var cities = await _cityDomainService.FindByIdsAsync(ids);
            return ObjectMapper.Map<List<Domain.Entities.City>, List<CityFetchingDto>>(cities);
        }

        [HttpGet]
        //[Route("api/app/City/Find")]
        public async Task<QueryResult<CityFetchingDto>> FindAsync(SearchCityDto criteria)
        {
             
            var searchCriteria = ObjectMapper.Map<SearchCityDto, CitySearchCriteria>(criteria);
            searchCriteria.IncludeCountry = true;
            searchCriteria.IsActive = true;
            var cities = await _cityDomainService.FindAsync(searchCriteria);
            return cities.MapToModel<Domain.Entities.City, CityFetchingDto>(ObjectMapper);
        }

        [HttpGet]
        [Route("api/app/city/{countryid}/getcities")]
        public async Task<List<CityFetchingDto>> GetListAsync(Guid countryId)
        {
            var cities = await _cityDomainService.FindByCountryAsync(countryId);
            return ObjectMapper.Map<List<Domain.Entities.City>, List<CityFetchingDto>>(cities);
        }

        #endregion
    }
}