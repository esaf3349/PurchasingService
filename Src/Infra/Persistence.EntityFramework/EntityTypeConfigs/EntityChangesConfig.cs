using Domain.Model.EntityChanges;
using Domain.Model.EntityChanges.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class EntityChangesConfig : BaseEntityConfig<EntityChange, Guid>
{
    public override void Configure(EntityTypeBuilder<EntityChange> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.EntityName).HasMaxLength(EntityNameConstants.MaxLength);
        builder.HasIndex(e => e.EntityName);

        builder.Property(e => e.EntityId).HasMaxLength(EntityIdConstants.MaxLength);
        builder.HasIndex(e => e.EntityId);

        builder.Property(e => e.PropertyName).HasMaxLength(PropertyNameConstants.MaxLength);
        builder.HasIndex(e => e.PropertyName);

        builder.HasIndex(e => e.PerformerId);

        builder.HasOne(e => e.Performer).WithMany(e => e.EntityChanges).HasForeignKey(e => e.PerformerId).OnDelete(DeleteBehavior.Restrict);
    }
}