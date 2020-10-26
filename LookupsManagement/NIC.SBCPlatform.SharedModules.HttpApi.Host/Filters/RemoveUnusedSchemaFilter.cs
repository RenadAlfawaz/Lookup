using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace NIC.SBCPlatform.SharedModules.Filters
{
    public class RemoveUnusedSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            string[] excludedSchemas = { "ControllerInterfaceApiDescriptionModel", "MethodParameterApiDescriptionModel", "ParameterApiDescriptionModel", "ReturnValueApiDescriptionModel", "ActionApiDescriptionModel", "ControllerApiDescriptionModel", "ModuleApiDescriptionModel", "PropertyApiDescriptionModel", "TypeApiDescriptionModel", "ApplicationApiDescriptionModel", "LanguageInfo", "DateTimeFormatDto", "CurrentCultureDto", "ApplicationLocalizationConfigurationDto", "ApplicationAuthConfigurationDto", "ApplicationSettingConfigurationDto", "CurrentUserDto", "ApplicationFeatureConfigurationDto", "MultiTenancyInfoDto", "CurrentTenantDto", "IanaTimeZone", "WindowsTimeZone", "TimeZone", "TimingDto", "ClockDto", "LocalizableStringDto", "ExtensionPropertyApiGetDto", "ExtensionPropertyApiCreateDto", "ExtensionPropertyApiUpdateDto", "ExtensionPropertyApiDto", "ExtensionPropertyUiTableDto", "ExtensionPropertyUiFormDto", "ExtensionPropertyUiDto", "ExtensionPropertyAttributeDto", "ExtensionPropertyDto", "EntityExtensionDto", "ModuleExtensionDto", "ExtensionEnumFieldDto", "ExtensionEnumDto", "ObjectExtensionsDto", "ApplicationConfigurationDto" };

            foreach (var item in context.SchemaRepository.Schemas.Keys.Where(ex => excludedSchemas.Contains(ex)))
                context.SchemaRepository.Schemas.Remove(item);
        }
    }
}
