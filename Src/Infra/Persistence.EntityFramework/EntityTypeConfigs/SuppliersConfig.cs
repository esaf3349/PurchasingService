using Domain.Model.Suppliers;
using Domain.Model.Suppliers.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class SuppliersConfig : BaseEntityConfig<Supplier, Guid>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);

        builder.Property(e => e.Description).HasMaxLength(DescriptionConstants.MaxLength);
    }
}