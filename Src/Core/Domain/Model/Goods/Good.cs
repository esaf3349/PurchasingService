using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Goods.Constants;
using Domain.Model.RequisitionLines;

namespace Domain.Model.Goods;

public sealed class Good : BaseEntity<Guid>
{
    private readonly HashSet<RequisitionLine> _requisitionLines = [];

    private Good() { }

    public Good(Guid id, string name, string? description) : base(id)
    {
        SetName(name);
        SetDescription(description);
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public IReadOnlyCollection<RequisitionLine> RequisitionLines => _requisitionLines;

    public void SetName(string name)
    {
        if (name.Length > NameConstants.MaxLength)
            throw new DomainException<Good>($"{nameof(Name)} should not be longer than {NameConstants.MaxLength} symbols");

        if (name.Length < NameConstants.MinLength)
            throw new DomainException<Good>($"{nameof(Name)} should be at least {NameConstants.MinLength} symbols");

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description?.Length > DescriptionContants.MaxLength)
            throw new DomainException<Good>($"{nameof(Description)} should not be longer than {DescriptionContants.MaxLength} symbols");

        Description = description;
    }
}