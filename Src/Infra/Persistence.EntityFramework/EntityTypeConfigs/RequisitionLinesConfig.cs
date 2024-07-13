using Domain.Model.RequisitionLines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.EntityTypeConfigs.Common;

namespace Persistence.EntityFramework.EntityTypeConfigs;

internal sealed class RequisitionLinesConfig : BaseEntityConfig<RequisitionLine, Guid>
{
    public override void Configure(EntityTypeBuilder<RequisitionLine> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Quantity).HasPrecision(19, 4);

        builder.Property(e => e.Amount).HasPrecision(19, 4);

        builder.Property(e => e.VatRate).HasPrecision(19, 4);

        builder.Property(e => e.VatAmount).HasPrecision(19, 4);

        builder.Property(e => e.AmountWithVat).HasPrecision(19, 4);

        builder.Property(e => e.TotalAmount).HasPrecision(19, 4);

        builder.Property(e => e.TotalVatAmount).HasPrecision(19, 4);

        builder.Property(e => e.TotalAmountWithVat).HasPrecision(19, 4);

        builder.HasOne(e => e.Requisition).WithMany(e => e.Lines).HasForeignKey(e => e.RequisitionId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Good).WithMany(e => e.RequisitionLines).HasForeignKey(e => e.GoodId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Measure).WithMany(e => e.RequisitionLines).HasForeignKey(e => e.MeasureId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency).WithMany(e => e.RequisitionLines).HasForeignKey(e => e.CurrencyId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.BudgetLine).WithMany(e => e.RequisitionLines).HasForeignKey(e => e.BudgetLineId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Warehouse).WithMany(e => e.RequisitionLines).HasForeignKey(e => e.WarehouseId).OnDelete(DeleteBehavior.Restrict);
    }
}