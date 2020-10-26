using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIC.SBCPlatform.SharedModules.LookupManagement.Country;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services;
using NIC.SBCPlatform.SharedModules.LookupManagement.EFCore;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Application.Services
{ 
    public class CountryAppService : LookupManagementAppService, ICountryAppService
    {
        #region private props

        private readonly ICountryDomainService _countryDomainService;
        private readonly ICityDomainService _cityDomainService;

        #endregion

        #region ctor

        public CountryAppService(ICountryDomainService countryDomainService, ICityDomainService cityDomainService)
        {
            _countryDomainService = countryDomainService;
            _cityDomainService = cityDomainService;
        }

        #endregion

        #region Admin Services
        //[Authorize("Admin")]
        [HttpPost]
        //[Route("api/app/admin/Country/Create")]
        public async Task CreateAsync(CountryCreationDto countryDto)
        {

            await _countryDomainService.CreateAsync(countryDto.EnglishName, countryDto.ArabicName, countryDto.IsActive);

        }
        //[Authorize("Admin")]
        [HttpPut]
        //[Route("api/app/admin/Country/Update")]
        public async Task UpdateAsync(CountryUpdateDto countryDto)
        {
            await _countryDomainService.UpdateAsync(countryDto.Id, countryDto.EnglishName, countryDto.ArabicName, countryDto.IsActive);
        }
        //[Authorize("Admin")]
        [HttpPut]
        //[Route("api/app/admin/Country/UpdateActivationStatus")]
        public async Task UpdateActivationStatusAsync(CountryUpdateActivationDto countryDto)
        {
            if (countryDto.IsActive == false)
            {
                var cities = await _cityDomainService.FindByCountryAsync(countryDto.Id);
                foreach (var city in cities)
                {
                    await _cityDomainService.UpdateActivationStatusAsync(city.Id, false);
                }

            }
            await _countryDomainService.UpdateActivationStatusAsync(countryDto.Id, countryDto.IsActive);

        }
        //[Authorize("Admin")]
        [HttpDelete]
        //[Route("api/app/admin/Country/Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _countryDomainService.DeleteAsync(id);
        }


        //[Authorize("Admin")]
        [HttpGet]
        //[Route("api/app/admin/Country/FindById")]
        public async Task<CountryDetailedFetchingDto> GetAsync(Guid id)
        {
            var country = await _countryDomainService.FindByIdAsync(id);
            return ObjectMapper.Map<Domain.Entities.Country, CountryDetailedFetchingDto>(country);
        }
        //[Authorize("Admin")]
        [HttpGet]
        //[Route("api/app/admin/Country/Find")]
        public async Task<QueryResult<CountryDetailedFetchingDto>> GetListAsync(CountrySearchCriteria criteria)
        {
            var countries = await _countryDomainService.FindAsync(criteria);
            return countries.MapToModel<Domain.Entities.Country, CountryDetailedFetchingDto>(ObjectMapper);
        }

        #endregion

        #region End Users Services

        [HttpGet]
        //[Route("api/app/Country/FindByIds")]
        public async Task<List<CountryFetchingDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var countries = await _countryDomainService.FindByIdsAsync(ids);
            return ObjectMapper.Map<List<Domain.Entities.Country>, List<CountryFetchingDto>>(countries);
        }

        [HttpGet]
        //[Route("api/app/Country/Find")]
        public async Task<QueryResult<CountryFetchingDto>> FindAsync(SearchCountryDto criteria)
        {
            var searchCriteria = ObjectMapper.Map<SearchCountryDto, CountrySearchCriteria>(criteria);
            searchCriteria.IsActive = true;
            var countries = await _countryDomainService.FindAsync(searchCriteria);
            return countries.MapToModel<Domain.Entities.Country, CountryFetchingDto>(ObjectMapper);
        }

        #endregion
    }
}
