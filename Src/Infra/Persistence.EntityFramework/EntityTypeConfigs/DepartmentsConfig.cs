using Domain.Model.Departments;
using Domain.Model.Departments.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class DepartmentsConfig : BaseEntityConfig<Department, Guid>
{
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(NameConstants.MaxLength);
    }
}