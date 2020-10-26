using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Data
{
    public interface ILookupManagementDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
