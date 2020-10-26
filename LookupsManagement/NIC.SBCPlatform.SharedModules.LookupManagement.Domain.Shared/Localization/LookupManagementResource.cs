using Volo.Abp.Localization;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Localization
{
    [LocalizationResourceName("LookupManagement")]
    public class LookupManagementResource
    {
        #region LookupDto

        public const string LookupEnglishName = "LookupEnglishName";
        public const string LookupArabicName = "LookupArabicName";
        public const string LookupIsActive = "LookupIsActive";
        public const string LookupName = "LookupName";
        public const string LookupType = "LookupType";

        #endregion

        #region CountryDto

        public const string CountryEnglishName = "CountryEnglishName";
        public const string CountryArabicName = "CountryArabicName";
        public const string CountryIsActive = "CountryIsActive";
        public const string CountryName = "CountryName";


        #endregion

        #region CityDto 

        public const string CityEnglishName = "CityEnglishName";
        public const string CityArabicName = "CityArabicName";
        public const string CityIsActive = "CityIsActive";
        public const string CityName = "CityName";
        public const string Country = "Country";

        #endregion
    }
}