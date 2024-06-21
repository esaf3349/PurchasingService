using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFramework.EntityTypeConfigs.Common;

internal abstract class BaseEntityConfig<TEntity, TEntityId> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TEntityId>
    where TEntityId : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}