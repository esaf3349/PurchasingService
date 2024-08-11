using Domain.Model.Permissions;
using Domain.Model.Permissions.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class PermissionsConfig : BaseEntityConfig<Permission, Guid>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.EntityIdFilter).HasMaxLength(EntityIdFilterConstants.MaxLength);

        builder.Property(e => e.PropertyFilter).HasMaxLength(PropertyFilterConstants.MaxLength);

        builder.HasOne(e => e.Role).WithMany(e => e.Permissions).HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Restrict);
    }
}