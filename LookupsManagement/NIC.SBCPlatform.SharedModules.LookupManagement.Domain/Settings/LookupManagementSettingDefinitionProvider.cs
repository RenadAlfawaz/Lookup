using Volo.Abp.Settings;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Settings
{
    public class LookupManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(LookupManagementSettings.MySetting1));
        }
    }
}
