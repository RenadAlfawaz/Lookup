using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.EntityFrameworkCore
{
    public static class LookupManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureLookupManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(LookupManagementConsts.DbTablePrefix + "YourEntities", LookupManagementConsts.DbSchema);

            //    //...
            //});
        }
    }
}