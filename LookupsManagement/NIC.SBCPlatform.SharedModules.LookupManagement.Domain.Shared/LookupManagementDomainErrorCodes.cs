namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public static class LookupManagementDomainErrorCodes
    {
        public static class ValidationErrors
        {
            /* You can add your business exception error codes here, as constants */
            public const string NotFoundException = "SharedModules.LookupManagement:1001";
            public const string EnglishNameException = "SharedModules.LookupManagement:1002";
            public const string ArabicNameException = "SharedModules.LookupManagement:1003";
            public const string CountryNotFoundException = "SharedModules.LookupManagement:1004";
            public const string UniqueNameException = "SharedModules.LookupManagement:1005";
            public const string CountryExistException = "SharedModules.LookupManagement:1006";
            public const string CityExistException = "SharedModules.LookupManagement:1007";
            public const string LookupExistException = "SharedModules.LookupManagement:1008";
            public const string LookupTypeNotFoundException = "SharedModules.LookupManagement:1009";

            public const string ErrorInEnteredFields = "SharedModules.LookupManagement:1010";
        } 

    }
}
