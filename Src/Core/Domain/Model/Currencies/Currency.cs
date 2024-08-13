using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Currencies.Constants;
using Domain.Model.RequisitionLines;

namespace Domain.Model.Currencies;

public sealed class Currency : BaseEntity<Guid>
{
    private readonly HashSet<RequisitionLine> _requisitionLines = [];

    public string Code { get; private set; }

    public IReadOnlyCollection<RequisitionLine> RequisitionLines => _requisitionLines;

    private Currency() { }

    public Currency(Guid id, string code) : base(id)
    {
        SetCode(code);
    }

    private void SetCode(string code)
    {
        if (code.Length != CodeConstants.ExactLength)
            throw new DomainException<Currency>($"{nameof(Code)} should be {CodeConstants.ExactLength} symbols");

        Code = code;
    }
}