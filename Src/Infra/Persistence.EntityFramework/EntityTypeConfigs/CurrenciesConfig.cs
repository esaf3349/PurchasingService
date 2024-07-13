using Domain.Model.Currencies;
using Domain.Model.Currencies.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class CurrenciesConfig : BaseEntityConfig<Currency, Guid>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Code).HasMaxLength(CodeConstants.ExactLength);
    }
}