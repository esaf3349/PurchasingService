using Domain.Model.Warehouses;
using Domain.Model.Warehouses.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class WarehousesConfig : BaseEntityConfig<Warehouse, Guid>
{
    public override void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);

        builder.Property(e => e.Address).HasMaxLength(AddressConstants.MaxLength);
    }
}