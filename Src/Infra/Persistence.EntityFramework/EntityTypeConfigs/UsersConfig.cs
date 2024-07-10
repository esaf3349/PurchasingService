using Domain.Model.Users;
using Domain.Model.Users.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class UsersConfig : BaseEntityConfig<User, Guid>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.FirstName).HasMaxLength(FirstNameConstants.MaxLength);

        builder.Property(e => e.LastName).HasMaxLength(LastNameConstants.MaxLength);

        builder.Property(e => e.MiddleName).HasMaxLength(MiddleNameConstants.MaxLength);

        builder.Property(e => e.Email).HasMaxLength(EmailConstants.MaxLength);
    }
}