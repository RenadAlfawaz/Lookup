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
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Validation;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Services
{
    public class LookupDomainService : DomainService, ILookupDomainService
    {
        #region private props

        private readonly ILookupRepository _lookupRepository;
        private readonly ILookupTypeRepository _lookupTypeRepository;
        private readonly IStringLocalizer<LookupManagementResource> _errorCodeslocalizer;
         

        #endregion

        #region ctor

        public LookupDomainService(ILookupRepository lookupRepository, ILookupTypeRepository lookupTypeRepository, IStringLocalizer<LookupManagementResource> errorCodeslocalizer)
        {
            _lookupRepository = lookupRepository;
            _lookupTypeRepository = lookupTypeRepository;
            _errorCodeslocalizer = errorCodeslocalizer;
        }

        #endregion

        #region Operations

        public async Task CreateAsync(string engName, string arName, Guid type, bool isActive)
        {

            var CreatedLookup = new Lookup(engName, arName, type, isActive);
            await DataValidationAsync(CreatedLookup);
            await _lookupRepository.InsertAsync(CreatedLookup, true);
        }

        public async Task UpdateAsync(Guid id, string engName, string arName, Guid type, bool isActive)
        {
            var entity = await _lookupRepository.FindAsync(id);

            if (entity == null)
            {
                throw new AbpValidationException(new List<ValidationResult>()
                {
                  new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException], new string[] { nameof(entity) })
                });
            }

            entity.Update(engName, arName, type, isActive);
            await DataValidationAsync(entity);

            await _lookupRepository.UpdateAsync(entity);
        }

        public async Task UpdateActivationStatusAsync(Guid id, bool isActive)
        {
            var entity = await _lookupRepository.FindAsync(id);

            if (entity == null)
            {
                throw new AbpValidationException(new List<ValidationResult>()
                {
                  new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException], new string[] { nameof(entity) })
                });
            }

            entity.UpdateActivation(isActive);

            await _lookupRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _lookupRepository.FindAsync(id);

            if (entity == null)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            await _lookupRepository.DeleteAsync(entity, true);
        }

        #endregion

        #region Find

        public async Task<Lookup> FindByIdAsync(Guid id)
        {
            var lookup = await _lookupRepository.FindAsync(id);
            if (lookup == null)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);

            return lookup;

        }

        public async Task<List<Lookup>> FindByTypeAsync(Guid type)
        {
            var lookups = await _lookupRepository.FindByTypeAsync(type);
            return lookups;
        }

        public async Task<List<Lookup>> FindByIdsAsync(List<Guid> ids)
        {
            var lookups = await _lookupRepository.FindByIdsAsync(ids);
            if (lookups.Count == 0)
                throw new EntityNotFoundException(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.NotFoundException]);
            return lookups;
        }

        public async Task<QueryResult<Lookup>> FindAsync(LookupSearchCriteria input)
        {
            var constraints = SetDefaultQueryConstraints(input);
            return await _lookupRepository.FindAsync(constraints);
        }

        public async Task<List<Lookup>> FindByTypesAsync(Guid type)
        {
            var lookups = await _lookupRepository.FindByTypesAsync(type);
            return lookups;
        }

        #endregion

        #region Private Methods

        private QueryConstraints<Lookup> SetDefaultQueryConstraints(LookupSearchCriteria criteria)
        {
            var constraints = new QueryConstraints<Lookup>().Page(criteria.PageNumber, criteria.PageSize);

            if (criteria.IncludeLookupType)
                constraints.AddInclude(a => a.LookupType);

            if (criteria.Id.HasValue)
                constraints.And(a => a.Id == criteria.Id);

            if (!string.IsNullOrEmpty(criteria.ArabicName))
                constraints.And(a => a.ArabicName == criteria.ArabicName);

            if (!string.IsNullOrEmpty(criteria.EnglishName))
                constraints.And(a => a.EnglishName == criteria.EnglishName);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                constraints.And(a => a.ArabicName == criteria.Name
                || a.EnglishName == criteria.Name);
            }

            if (criteria.LookupType.HasValue)
                constraints.And(a => a.LookupTypeId == criteria.LookupType);

            if (criteria.IsActive.HasValue)
                constraints.And(a => a.IsActive == criteria.IsActive);

            constraints.Order(criteria.Sort, criteria.SortDirection);

            return constraints;
        }

        private QueryConstraints<Lookup> NameQueryConstraints(LookupSearchCriteria criteria)
        {
            var constraints = new QueryConstraints<Lookup>().Page(criteria.PageNumber, criteria.PageSize);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                constraints.And(a => a.ArabicName == criteria.Name
                || a.EnglishName == criteria.Name);
            }

            constraints.Order(criteria.Sort, criteria.SortDirection);

            return constraints;
        }

        private async Task DataValidationAsync(Lookup checkLookup)
        {
            var validationResults = new List<ValidationResult>();
            ValidateArabicName(checkLookup, validationResults);
            ValidateEnglishName(checkLookup, validationResults);
            await CheckIsLookupTypeExist(checkLookup, validationResults);
            await CheckIsExist(checkLookup, validationResults);

            if (validationResults.Count > 0)
                throw new AbpValidationException(LookupManagementDomainErrorCodes.ValidationErrors.ErrorInEnteredFields, validationResults);
        }

        private void ValidateArabicName(Lookup checkLookup, List<ValidationResult> validationResults)
        {
            if (Util.IsArabic(checkLookup.ArabicName) == false)
                validationResults.Add(
                     new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.ArabicNameException], new string[] { nameof(checkLookup.ArabicName) }));
        }

        private void ValidateEnglishName(Lookup checkLookup , List<ValidationResult> validationResults)
        {
            if (Util.IsEnglish(checkLookup.EnglishName) == false)
                validationResults.Add(
                       new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.EnglishNameException], new string[] { nameof(checkLookup.EnglishName) }));
        }

        private async Task CheckIsExist(Lookup checkLookup, List<ValidationResult> validationResults)
        {
            var list = await _lookupRepository.GetListAsync();
            var isExist = list.Where(a =>
            a.LookupTypeId == checkLookup.LookupTypeId && (checkLookup.EnglishName == a.EnglishName || checkLookup.ArabicName == a.ArabicName)
             && checkLookup.Id != a.Id).ToList();

            if (isExist.Count != 0)
                if (isExist.Count != 0)
                    validationResults.Add(new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.LookupExistException]));
        }

        private async Task CheckIsLookupTypeExist(Lookup checkLookup, List<ValidationResult> validationResults)
        {
            var isExist = await _lookupTypeRepository.FindAsync(checkLookup.LookupTypeId);
            if (isExist == null)
                if (isExist == null)
                    validationResults.Add(
                          new ValidationResult(_errorCodeslocalizer[LookupManagementDomainErrorCodes.ValidationErrors.LookupTypeNotFoundException], new string[] { nameof(checkLookup.LookupType) }));
        }
        #endregion

    }
}
