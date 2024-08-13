using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Permissions.Constants;
using Domain.Model.Permissions.ValueObjects;
using Domain.Model.Roles;

namespace Domain.Model.Permissions;

public sealed class Permission : BaseEntity<Guid>
{
    public Guid RoleId { get; private init; }
    public Role? Role { get; }
    public AllowedEntity? EntityFilter { get; private init; }
    public string? EntityIdFilter { get; private init; }
    public string? PropertyFilter { get; private init; }
    public AllowedAction? ActionFilter { get; private init; }

    private Permission() { }

    public Permission(Guid id, AllowedEntity? entityFilter, string? entityIdFilter, string? propertyFilter, AllowedAction? actionFilter) : base(id)
    {
        if (entityIdFilter?.Length > EntityIdFilterConstants.MaxLength)
            throw new DomainException<Permission>($"{nameof(EntityIdFilter)} should not be longer than {EntityIdFilterConstants.MaxLength} symbols");

        if (entityIdFilter?.Length < EntityIdFilterConstants.MinLength)
            throw new DomainException<Permission>($"{nameof(EntityIdFilter)} should be at least {EntityIdFilterConstants.MaxLength} symbols");

        if (!string.IsNullOrWhiteSpace(propertyFilter) && EntityFilter == null)
            throw new DomainException<Permission>($"{nameof(PropertyFilter)} requires {nameof(EntityFilter)}");

        if (!string.IsNullOrWhiteSpace(entityIdFilter) && EntityFilter == null)
            throw new DomainException<Permission>($"{nameof(EntityIdFilter)} requires {nameof(EntityFilter)}");

        if (actionFilter != null && actionFilter != AllowedAction.Read && EntityFilter == null)
            throw new DomainException<Permission>($"{nameof(ActionFilter)} requires {nameof(EntityFilter)}");

        EntityFilter = entityFilter;
        EntityIdFilter = entityIdFilter;
        PropertyFilter = propertyFilter;
        ActionFilter = actionFilter;
    }
}