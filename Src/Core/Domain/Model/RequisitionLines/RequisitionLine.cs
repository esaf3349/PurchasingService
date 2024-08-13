using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.BudgetLines;
using Domain.Model.Currencies;
using Domain.Model.Goods;
using Domain.Model.Measures;
using Domain.Model.RequisitionLines.ValueObjects;
using Domain.Model.Requisitions;
using Domain.Model.Warehouses;

namespace Domain.Model.RequisitionLines;

public sealed class RequisitionLine : BaseEntity<Guid>
{
    public Guid RequisitionId { get; private init; }
    public Requisition? Requisition { get; }
    public int OrdinalNumber { get; private init; }
    public Guid GoodId { get; private set; }
    public Good? Good { get; }
    public Guid MeasureId { get; private set; }
    public Measure? Measure { get; }
    public decimal Quantity { get; private set; }
    public Guid CurrencyId { get; private set; }
    public Currency? Currency { get; }
    public decimal Amount { get; private set; }
    public decimal VatRate { get; private set; }
    public decimal VatAmount { get; private set; }
    public decimal AmountWithVat { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal TotalVatAmount { get; private set; }
    public decimal TotalAmountWithVat { get; private set; }
    public Guid BudgetLineId { get; private set; }
    public BudgetLine? BudgetLine { get; }
    public Guid WarehouseId { get; private set; }
    public Warehouse? Warehouse { get; }

    private RequisitionLine() { }

    public RequisitionLine(Guid id, int ordinalNumber, Guid goodId, Guid measureId, decimal quantity, Price price, Guid budgetLineId, Guid warehouseId) : base(id)
    {
        if (ordinalNumber < 1)
            throw new DomainException<RequisitionLine>($"{nameof(OrdinalNumber)} should be positive");

        OrdinalNumber = ordinalNumber;
        GoodId = goodId;
        MeasureId = measureId;

        SetPrice(quantity, price);

        BudgetLineId = budgetLineId;
        WarehouseId = warehouseId;
    }

    public void SetPrice(decimal quantity, Price price)
    {
        if (quantity <= 0)
            throw new DomainException<RequisitionLine>($"{nameof(Quantity)} should be positive");

        Quantity = quantity;

        Amount = price.Amount;
        VatRate = price.VatRate;
        VatAmount = price.VatAmount;
        AmountWithVat = price.AmountWithVat;
        TotalAmount = price.Amount * quantity;
        TotalVatAmount = price.VatAmount * quantity;
        TotalAmountWithVat = price.AmountWithVat * quantity;
    }
}