using Microsoft.Extensions.Localization;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Helpers;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Interfaces;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.LookupManagement.Localization;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Validation;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public class CityDomainService : DomainService, ICityDomainService
    {
        #region private props

        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStringLocalizer<LookupManagementResource> _errorCodeslocalizer;

        #endregion

        #region ctor

        public CityDomainService(ICityRepository cityRepository, IStringLocalizer<LookupManagementResource> errorCodeslocalizer, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _errorCodeslocalizer = errorCodeslocalizer;
            _countryRepository = countryRepository;
        }

        #endregion

        #region Operations

        public async Task CreateAsync(string engName, string arName, Guid countryId, bool isActive)
        {

            var CreateCity = new City(engName, arName, countryId, isActive);
            await DataValidationAsync(CreateCity); 
            await _cityRepository.InsertAsync(CreateCity, true);
        }

        public async Task UpdateAsync(Guid id, string engName, string arName, Guid countryId, bool isActive)
        {
            var entity = await _cityRepository.FindAsync(id);

            if (entity == null)
            {
                throw new AbpValidationException(new List<ValidationResult>()
                {
                  new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException], new string[] { nameof(entity) })
                });
            } 

            entity.Update(engName, arName, countryId, isActive);
            await DataValidationAsync(entity);
             
            await _cityRepository.UpdateAsync(entity);
        }

        public async Task UpdateActivationStatusAsync(Guid id, bool isActive)
        {
            var entity = await _cityRepository.FindAsync(id);

            if (entity == null)
            {
                throw new AbpValidationException(new List<ValidationResult>()
                {
                  new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException], new string[] { nameof(entity) })
                });
            } 

            entity.UpdateActivation(isActive);

            await _cityRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _cityRepository.FindAsync(id);

            if (entity == null)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            await _cityRepository.DeleteAsync(entity, true);
        }

        #endregion

        #region Find

        public async Task<City> FindByIdAsync(Guid id)
        {
            var city = await _cityRepository.FindAsync(id);

            if (city == null)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);
              
            return city;

        }

        public async Task<List<City>> FindByCountryAsync(Guid countryId)
        {
            var cities = await _cityRepository.FindByCountryAsync(countryId);
            return cities;
        }

        public async Task<List<City>> FindByIdsAsync(List<Guid> ids)
        {
            var cities = await _cityRepository.FindByIdsAsync(ids);

            if (cities.Count == 0)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            return cities;
        }

        public async Task<QueryResult<City>> FindAsync(CitySearchCriteria input)
        {
            var constraints = SetDefaultQueryConstraints(input);
            return await _cityRepository.FindAsync(constraints);
        }

        #endregion

        #region Private Methods

        private QueryConstraints<City> SetDefaultQueryConstraints(CitySearchCriteria criteria)
        {
            var constraints = new QueryConstraints<City>().Page(criteria.PageNumber, criteria.PageSize);

            constraints.AddInclude(a => a.Country);

            if (criteria.Id.HasValue)
                constraints.And(a => a.Id == criteria.Id);

            if (!string.IsNullOrEmpty(criteria.ArabicName))
                constraints.And(a => a.ArabicName.Contains(criteria.ArabicName));

            if (!string.IsNullOrEmpty(criteria.EnglishName))
                constraints.And(a => a.EnglishName.Contains(criteria.EnglishName));

            if (!string.IsNullOrEmpty(criteria.Name))
                constraints.And(a => a.ArabicName.Contains(criteria.Name) || a.EnglishName.Contains(criteria.Name));

            if (criteria.Country.HasValue)
                constraints.And(a => a.CountryId == criteria.Country);

            if (criteria.IsActive.HasValue)
                constraints.And(a => a.IsActive == criteria.IsActive);


            constraints.Order(criteria.Sort, criteria.SortDirection);

            return constraints;
        }

        private QueryConstraints<City> NameQueryConstraints(CitySearchCriteria criteria)
        {
            var constraints = new QueryConstraints<City>().Page(criteria.PageNumber, criteria.PageSize);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                constraints.And(a => a.ArabicName == criteria.Name
                || a.EnglishName == criteria.Name);
            }

            constraints.Order(criteria.Sort, criteria.SortDirection);

            return constraints;
        }

        public async Task DataValidationAsync(City checkCity)
        {
            var validationResults = new List<ValidationResult>();
            //ValidateArabicName(checkCity, validationResults); 
            //ValidateEnglishName(checkCity, validationResults);
            await CheckIsCountryExist(checkCity, validationResults);
            await CheckIsExist(checkCity, validationResults);


            if (validationResults.Count > 0)
                throw new AbpValidationException(LookupManagementDomainErrorCodes.ValidationErrors.ErrorInEnteredFields, validationResults);
        }

        private async Task CheckIsCountryExist(City checkCity, List<ValidationResult> validationResults)
        {
            var isExist = await _countryRepository.FindAsync(checkCity.CountryId);  
            if (isExist == null)
                validationResults.Add(
                      new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.CountryNotFoundException], new string[] { nameof(checkCity.Country) }));
        }

        private void ValidateArabicName(City checkCity, List<ValidationResult> validationResults)
        {
            if (Util.IsArabic(checkCity.ArabicName) == false)
                validationResults.Add(
                   new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException], new string[] { nameof(checkCity.ArabicName) }));
        }

        private void ValidateEnglishName(City checkCity, List<ValidationResult> validationResults)
        {
            if (Util.IsEnglish(checkCity.EnglishName) == false)
                validationResults.Add(
                        new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException], new string[] { nameof(checkCity.EnglishName) }));
        
        }

        private async Task CheckIsExist(City checkCity, List<ValidationResult> validationResults)
        {
            var list = await _cityRepository.GetListAsync();
            var isExist = list.Where(a =>
            a.CountryId == checkCity.CountryId && (checkCity.EnglishName == a.EnglishName || checkCity.ArabicName == a.ArabicName)
             && checkCity.Id != a.Id).ToList();

            if (isExist.Count != 0)
                validationResults.Add(new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.CityExistException]));
        }

        #endregion
    }
}
