using Domain.Common.Exceptions;

namespace Domain.Model.RequisitionLines.ValueObjects;

public sealed record Price
{
    public Guid CurrencyId { get; private set; }
    public decimal Amount { get; private set; }
    public decimal VatRate { get; private set; }
    public decimal VatAmount { get; private set; }
    public decimal AmountWithVat { get; private set; }

    private Price() { }

    public Price(Guid currencyId, decimal amount, decimal vatRate)
    {
        if (amount < 0)
            throw new DomainException<RequisitionLine>($"{nameof(Amount)} should not be negative");

        if (vatRate < 0)
            throw new DomainException<RequisitionLine>($"{nameof(VatRate)} should not be negative");

        Amount = amount;
        VatRate = vatRate;
        VatAmount = vatRate / 100 * amount;
        AmountWithVat = Amount + VatAmount;
    }
}