using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NIC.SBCPlatform.SharedModules.LookupManagement.Data;
using Volo.Abp.DependencyInjection;

namespace NIC.SBCPlatform.SharedModules.EntityFrameworkCore
{
    public class EntityFrameworkCoreLookupManagementDbSchemaMigrator
        : ILookupManagementDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreLookupManagementDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the LookupManagementMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<MigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}