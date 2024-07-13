﻿using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Goods.Constants;
using Domain.Model.RequisitionLines;

namespace Domain.Model.Goods;

public sealed class Good : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public ICollection<RequisitionLine> RequisitionLines { get; private set; }

    private Good() { }

    public Good(string name, string? description)
    {
        Name = name;
        Description = description;
    }

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