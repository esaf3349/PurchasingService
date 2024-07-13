using Domain.Model.Requisitions;
using Domain.Model.Requisitions.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class RequisitionsConfig : BaseEntityConfig<Requisition, Guid>
{
    public override void Configure(EntityTypeBuilder<Requisition> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title).HasMaxLength(TitleConstants.MaxLength);

        builder.HasOne(e => e.Supplier).WithMany(e => e.Requisitions).HasForeignKey(e => e.SupplierId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Department).WithMany(e => e.Requisitions).HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Requester).WithMany(e => e.Requisitions).HasForeignKey(e => e.RequesterId).OnDelete(DeleteBehavior.Restrict);
    }
}