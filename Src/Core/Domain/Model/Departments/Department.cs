using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Departments.Constants;
using Domain.Model.Requisitions;

namespace Domain.Model.Departments;

public sealed class Department : BaseEntity<Guid>
{
    private readonly HashSet<Requisition> _requisitions = [];

    public string Name { get; private set; }

    public IReadOnlyCollection<Requisition> Requisitions => _requisitions;

    private Department() { }

    public Department(Guid id, string name) : base(id)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (name.Length > NameConstants.MaxLength)
            throw new DomainException<Department>($"{nameof(Name)} should not be longer than {NameConstants.MaxLength} symbols");

        if (name.Length < NameConstants.MinLength)
            throw new DomainException<Department>($"{nameof(Name)} should be at least {NameConstants.MinLength} symbols");

        Name = name;
    }
}