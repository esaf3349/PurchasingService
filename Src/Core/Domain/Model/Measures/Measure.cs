using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Measures.Constants;
using Domain.Model.RequisitionLines;

namespace Domain.Model.Measures;

public sealed class Measure : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public ICollection<RequisitionLine> RequisitionLines { get; private set; }

    private Measure() { }

    public Measure(Guid id, string name, string? description) : base(id)
    {
        Id = id;
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (name.Length > NameConstants.MaxLength)
            throw new DomainException<Measure>($"{nameof(Name)} should not be longer than {NameConstants.MaxLength} symbols");

        if (name.Length < NameConstants.MinLength)
            throw new DomainException<Measure>($"{nameof(Name)} should be at least {NameConstants.MinLength} symbols");

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description?.Length > DescriptionContants.MaxLength)
            throw new DomainException<Measure>($"{nameof(Description)} should not be longer than {DescriptionContants.MaxLength} symbols");

        Description = description;
    }
}