using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Data
{
    /* This is used if database provider does't define
     * ILookupManagementDbSchemaMigrator implementation.
     */
    public class NullLookupManagementDbSchemaMigrator : ILookupManagementDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}