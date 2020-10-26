using AutoMapper;
using NIC.SBCPlatform.SharedModules.LookupManagement.City;
using NIC.SBCPlatform.SharedModules.LookupManagement.Country;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Domain.SearchCriteria;
using NIC.SBCPlatform.SharedModules.LookupManagement.Entities;
using NIC.SBCPlatform.SharedModules.LookupManagement.Lookup;
using System.Threading;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class LookupManagementApplicationAutoMapperProfile : Profile
    {
        public LookupManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            //bool isEnglish = ;
             

            CreateMap<Request, RequestDto>().ReverseMap()
                .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<Domain.Entities.Lookup, LookupDetailedFetchingDto>()
              .ForMember(a => a.LookupType, f => f.MapFrom(m => m.LookupType ?? null))
              .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
              .ReverseMap()
             .ForMember(x => x.CreatorId, 
             act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());
            
            CreateMap<Domain.Entities.Lookup, LookupCreationDto>()
                .ReverseMap()
                .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<Domain.Entities.Lookup, LookupFetchingDto>()
               .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
               .ReverseMap()
               .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<SearchLookupDto, LookupSearchCriteria>()
              .ReverseMap();

            CreateMap<Domain.Entities.Country, CountryCreationDto>()
               .ReverseMap()
               .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<Domain.Entities.Country, CountryDetailedFetchingDto>()
               .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
               .ReverseMap()
               .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<Domain.Entities.Country, CountryFetchingDto>()
             .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
             .ReverseMap()
             .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<SearchCountryDto, CountrySearchCriteria>()
              .ReverseMap();

            CreateMap<Domain.Entities.City, CityCreationDto>()
               .ReverseMap()
               .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<Domain.Entities.City, CityDetailedFetchingDto>()
               .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
               .ForMember(a => a.Country, f => f.MapFrom(m => m.Country ?? null))
               .ReverseMap()
               .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore())
               .ForMember(x => x.LastModificationTime, act => act.Ignore()).ForMember(x => x.LastModifierId, act => act.Ignore())
               ;

            CreateMap<Domain.Entities.City, CityFetchingDto>()
             .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
             .ReverseMap()
             .ForMember(x => x.CreatorId, act => act.Ignore()).ForMember(x => x.CreationTime, act => act.Ignore());

            CreateMap<SearchCityDto, CitySearchCriteria>()
              .ReverseMap();

            CreateMap<LookupType, LookupTypeFetchingDto>()
             .ForMember(a => a.Name, f => f.MapFrom(m => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Contains("en")
                                                            ? m.EnglishName : m.ArabicName))
             .ReverseMap(); 
        }
    }
}
