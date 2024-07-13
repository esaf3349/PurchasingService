using Domain.Model.Measures;
using Domain.Model.Measures.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class MeasuresConfig : BaseEntityConfig<Measure, Guid>
{
    public override void Configure(EntityTypeBuilder<Measure> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);

        builder.Property(e => e.Description).HasMaxLength(DescriptionContants.MaxLength);
    }
}