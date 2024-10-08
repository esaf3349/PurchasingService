﻿using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.EntityChanges.Constants;
using Domain.Model.Users;

namespace Domain.Model.EntityChanges;

public sealed class EntityChange : BaseEntity<Guid>
{
    private EntityChange() { }

    public EntityChange(Guid id, string entityName, string entityId, string propertyName, string? oldValue, string? newValue, Guid? performerId) : base(id)
    {
        if (entityName.Length > EntityNameConstants.MaxLength)
            throw new DomainException<EntityChange>($"{nameof(EntityName)} should not be longer than {EntityNameConstants.MaxLength} symbols");

        if (entityName.Length < EntityNameConstants.MinLength)
            throw new DomainException<EntityChange>($"{nameof(EntityName)} should be at least {EntityNameConstants.MinLength} symbols");

        if (entityId.Length > EntityIdConstants.MaxLength)
            throw new DomainException<EntityChange>($"{nameof(EntityId)} should not be longer than {EntityIdConstants.MaxLength} symbols");

        if (entityId.Length < EntityIdConstants.MinLength)
            throw new DomainException<EntityChange>($"{nameof(EntityId)} should be at least {EntityIdConstants.MinLength} symbols");

        if (propertyName.Length > PropertyNameConstants.MaxLength)
            throw new DomainException<EntityChange>($"{nameof(PropertyName)} should not be longer than {PropertyNameConstants.MaxLength} symbols");

        if (propertyName.Length < PropertyNameConstants.MinLength)
            throw new DomainException<EntityChange>($"{nameof(PropertyName)} should be at least {PropertyNameConstants.MinLength} symbols");

        EntityName = entityName;
        EntityId = entityId;
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
        PerformerId = performerId;
    }

    public string EntityName { get; private init; }
    public string EntityId { get; private init; }
    public string PropertyName { get; private init; }
    public string? OldValue { get; private init; }
    public string? NewValue { get; private init; }
    public Guid? PerformerId { get; private init; }
    public User? Performer { get; }
}