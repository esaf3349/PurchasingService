using Domain.Model.RolePermissions;
using Domain.Model.RolePermissions.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class RolePermissionsConfig : BaseEntityConfig<RolePermission, Guid>
{
    public override void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.EntityIdPermissionFilter).HasMaxLength(EntityIdPermissionFilterConstants.MaxLength);

        builder.Property(e => e.PropertyPermissionFilter).HasMaxLength(PropertyPermissionFilterConstants.MaxLength);

        builder.HasOne(e => e.Role).WithMany(e => e.Permissions).HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Restrict);
    }
}