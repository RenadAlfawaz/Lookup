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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Volo.Abp.Validation;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public class CountryDomainService : DomainService, ICountryDomainService
    {
        #region private props

        private readonly ICountryRepository _countryRepository;
        private readonly IStringLocalizer<LookupManagementResource> _errorCodeslocalizer;

        #endregion

        #region ctor

        public CountryDomainService(ICountryRepository countryRepository, IStringLocalizer<LookupManagementResource> errorCodeslocalizer)
        {
            _countryRepository = countryRepository;
            _errorCodeslocalizer = errorCodeslocalizer;
        }

        #endregion

        #region Operations

        public async Task CreateAsync(string engName, string arName, bool isActive)
        {
            var createCountry = new Country(engName, arName, isActive);
            await DataValidationAsync(createCountry);

            await _countryRepository.InsertAsync(createCountry, true);
        }

        public async Task UpdateAsync(Guid id, string engName, string arName, bool isActive)
        {
            var entity = await _countryRepository.FindAsync(id);

            if (entity == null)
            {
                throw new AbpValidationException(new List<ValidationResult>()
                {
                  new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException], new string[] { nameof(entity) })
                });
            }
            entity.Update(engName, arName, isActive);
            await DataValidationAsync(entity);

            await _countryRepository.UpdateAsync(entity);
        }

        public async Task UpdateActivationStatusAsync(Guid id, bool isActive)
        {
            var entity = await _countryRepository.FindAsync(id);

            if (entity == null)
            {
                throw new AbpValidationException(new List<ValidationResult>()
                {
                  new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException], new string[] { nameof(entity) })
                });
            }

            entity.UpdateActivation(isActive);


            await _countryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _countryRepository.FindAsync(id);

            if (entity == null)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            await _countryRepository.DeleteAsync(entity, true);
        }

        #endregion

        #region Find

        public async Task<Country> FindByIdAsync(Guid id)
        {
            var country = await _countryRepository.FindAsync(id);

            if (country == null)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            return country;

        }

        public async Task<List<Country>> FindByIdsAsync(List<Guid> ids)
        {
            var countries = await _countryRepository.FindByIdsAsync(ids);

            if (countries.Count == 0)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            return countries;
        }

        public async Task<QueryResult<Country>> FindAsync(CountrySearchCriteria input)
        {
            var constraints = SetDefaultQueryConstraints(input);
            return await _countryRepository.FindAsync(constraints);
        }

        #endregion

        #region Private Methods

        private QueryConstraints<Country> SetDefaultQueryConstraints(CountrySearchCriteria criteria)
        {
            var constraints = new QueryConstraints<Country>().Page(criteria.PageNumber, criteria.PageSize);

            if (criteria.Id.HasValue)
                constraints.And(a => a.Id == criteria.Id);

            if (!string.IsNullOrEmpty(criteria.ArabicName))
                constraints.And(a => a.ArabicName.Contains(criteria.ArabicName));

            if (!string.IsNullOrEmpty(criteria.EnglishName))
                constraints.And(a => a.EnglishName.Contains(criteria.EnglishName));

            if (!string.IsNullOrEmpty(criteria.Name))
                constraints.And(a => a.ArabicName.Contains(criteria.Name) || a.EnglishName.Contains(criteria.Name));

            if (criteria.IsActive.HasValue)
                constraints.And(a => a.IsActive == criteria.IsActive);


            constraints.Order(criteria.Sort, criteria.SortDirection);

            return constraints;
        }

        private QueryConstraints<Country> NameQueryConstraints(CountrySearchCriteria criteria)
        {
            var constraints = new QueryConstraints<Country>().Page(criteria.PageNumber, criteria.PageSize);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                constraints.And(a => a.ArabicName.Contains(criteria.Name) || a.EnglishName.Contains(criteria.Name));
            }

            constraints.Order(criteria.Sort, criteria.SortDirection);

            return constraints;
        }

        public async Task DataValidationAsync(Country checkCountry)
        {
            var validationResults = new List<ValidationResult>();
            ValidateArabicName(checkCountry, validationResults);
            ValidateEnglishName(checkCountry, validationResults);
            await CheckIsExist(checkCountry, validationResults);

            if (validationResults.Count > 0)
                throw new AbpValidationException(LookupManagementDomainErrorCodes.ValidationErrors.ErrorInEnteredFields, validationResults);
        }


        private void ValidateArabicName(Country checkCountry, List<ValidationResult> validationResults)
        {
            if (Util.IsArabic(checkCountry.ArabicName) == false)
                validationResults.Add(
                    new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException], new string[] { nameof(checkCountry.ArabicName) }));
        }

        private void ValidateEnglishName(Country checkCountry, List<ValidationResult> validationResults)
        {
            if (Util.IsEnglish(checkCountry.EnglishName) == false)
                validationResults.Add(new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException], new string[] { nameof(checkCountry.EnglishName) }));           
        }

        private async Task CheckIsExist(Country checkCountry, List<ValidationResult> validationResults)
        { 
            var list = await _countryRepository.GetListAsync();
            var isExist = list.Where(a => (checkCountry.EnglishName == a.EnglishName || checkCountry.ArabicName == a.ArabicName)
             && checkCountry.Id != a.Id).ToList();

            if (isExist.Count != 0)
                validationResults.Add(new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.CountryExistException]));
        }

        #endregion
    }
}
