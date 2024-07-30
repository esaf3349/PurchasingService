using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Roles.Constants;

namespace Domain.Model.Roles;

public sealed class Role : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool ForSingleUser { get; private set; }

    private Role() { }

    public Role(Guid id, string name, string? description, bool forSingleUser = false) : base(id)
    {
        SetName(name);
        SetDescription(description);

        ForSingleUser = forSingleUser;
    }

    public void SetName(string name)
    {
        if (name.Length > NameConstants.MaxLength)
            throw new DomainException<Role>($"{nameof(Name)} should not be longer than {NameConstants.MaxLength} symbols");

        if (name.Length < NameConstants.MinLength)
            throw new DomainException<Role>($"{nameof(Name)} should be at least {NameConstants.MinLength} symbols");

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description?.Length > DescriptionContants.MaxLength)
            throw new DomainException<Role>($"{nameof(Description)} should not be longer than {DescriptionContants.MaxLength} symbols");

        Description = description;
    }
}