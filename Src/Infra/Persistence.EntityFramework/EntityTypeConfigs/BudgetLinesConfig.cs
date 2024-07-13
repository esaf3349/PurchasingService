using Domain.Model.BudgetLines;
using Domain.Model.BudgetLines.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class BudgetLinesConfig : BaseEntityConfig<BudgetLine, Guid>
{
    public override void Configure(EntityTypeBuilder<BudgetLine> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);

        builder.Property(e => e.Description).HasMaxLength(DescriptionContants.MaxLength);
    }
}