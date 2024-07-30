using Domain.Model.Roles;
using Domain.Model.Roles.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class RolesConfig : BaseEntityConfig<Role, Guid>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);

        builder.Property(e => e.Description).HasMaxLength(DescriptionContants.MaxLength);
    }
}