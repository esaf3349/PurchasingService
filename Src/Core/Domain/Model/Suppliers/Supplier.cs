using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Requisitions;
using Domain.Model.Suppliers.Constants;

namespace Domain.Model.Suppliers;

public sealed class Supplier : BaseEntity<Guid>
{
    public string Title { get; private set; }
    public string? Description { get; private set; }

    public ICollection<Requisition> Requisitions { get; private set; }

    private Supplier() { }

    public Supplier(Guid id, string title, string? description) : base(id)
    {
        Id = id;
        SetTitle(title);
        SetDescription(description);
    }

    public void SetTitle(string title)
    {
        if (title.Length > TitleConstants.MaxLength)
            throw new DomainException<Supplier>($"{nameof(Title)} should not be longer than {TitleConstants.MaxLength} symbols");

        if (title.Length < TitleConstants.MinLength)
            throw new DomainException<Supplier>($"{nameof(Title)} should be at least {TitleConstants.MinLength} symbols");

        Title = title;
    }

    public void SetDescription(string? description)
    {
        if (description?.Length > DescriptionContants.MaxLength)
            throw new DomainException<Supplier>($"{nameof(Description)} should not be longer than {DescriptionContants.MaxLength} symbols");

        Description = description;
    }
}