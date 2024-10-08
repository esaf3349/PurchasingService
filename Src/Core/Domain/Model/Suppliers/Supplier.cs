﻿using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Requisitions;
using Domain.Model.Suppliers.Constants;

namespace Domain.Model.Suppliers;

public sealed class Supplier : BaseEntity<Guid>
{
    private readonly HashSet<Requisition> _requisitions = [];

    private Supplier() { }

    public Supplier(Guid id, string name, string? description) : base(id)
    {
        SetName(name);
        SetDescription(description);
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public IReadOnlyCollection<Requisition> Requisitions => _requisitions;

    public void SetName(string name)
    {
        if (name.Length > NameConstants.MaxLength)
            throw new DomainException<Supplier>($"{nameof(Name)} should not be longer than {NameConstants.MaxLength} symbols");

        if (name.Length < NameConstants.MinLength)
            throw new DomainException<Supplier>($"{nameof(Name)} should be at least {NameConstants.MinLength} symbols");

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description?.Length > DescriptionConstants.MaxLength)
            throw new DomainException<Supplier>($"{nameof(Description)} should not be longer than {DescriptionConstants.MaxLength} symbols");

        Description = description;
    }
}