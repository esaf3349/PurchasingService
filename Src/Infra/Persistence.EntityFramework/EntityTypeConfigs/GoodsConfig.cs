using Domain.Model.Goods;
using Domain.Model.Goods.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class GoodsConfig : BaseEntityConfig<Good, Guid>
{
    public override void Configure(EntityTypeBuilder<Good> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);

        builder.Property(e => e.Description).HasMaxLength(DescriptionContants.MaxLength);
    }
}