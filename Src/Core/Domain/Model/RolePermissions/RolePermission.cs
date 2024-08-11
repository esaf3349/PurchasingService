using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.RolePermissions.Constants;
using Domain.Model.RolePermissions.ValueObjects;
using Domain.Model.Roles;

namespace Domain.Model.RolePermissions;

public sealed class RolePermission : BaseEntity<Guid>
{
    public Guid RoleId { get; }
    public Role? Role { get; }
    public AllowedEntity? EntityPermissionFilter { get; private init; }
    public string? EntityIdPermissionFilter { get; private init; }
    public string? PropertyPermissionFilter { get; private init; }
    public AllowedAction? ActionPermissionFilter { get; private init; }

    private RolePermission() { }

    public RolePermission(Guid id, AllowedEntity? entityPermissionFilter, string? entityIdPermissionFilter, string? propertyPermissionFilter, AllowedAction? actionPermissionFilter) : base(id)
    {
        if (entityIdPermissionFilter?.Length > EntityIdPermissionFilterConstants.MaxLength)
            throw new DomainException<RolePermission>($"{nameof(EntityIdPermissionFilter)} should not be longer than {EntityIdPermissionFilterConstants.MaxLength} symbols");

        if (entityIdPermissionFilter?.Length < EntityIdPermissionFilterConstants.MinLength)
            throw new DomainException<RolePermission>($"{nameof(EntityIdPermissionFilter)} should be at least {EntityIdPermissionFilterConstants.MaxLength} symbols");

        if (!string.IsNullOrWhiteSpace(propertyPermissionFilter) && EntityPermissionFilter == null)
            throw new DomainException<RolePermission>($"{nameof(PropertyPermissionFilter)} requires {nameof(EntityPermissionFilter)}");

        if (!string.IsNullOrWhiteSpace(entityIdPermissionFilter) && EntityPermissionFilter == null)
            throw new DomainException<RolePermission>($"{nameof(EntityIdPermissionFilter)} requires {nameof(EntityPermissionFilter)}");

        if (actionPermissionFilter != null && actionPermissionFilter != AllowedAction.Read && EntityPermissionFilter == null)
            throw new DomainException<RolePermission>($"{nameof(ActionPermissionFilter)} requires {nameof(EntityPermissionFilter)}");

        EntityPermissionFilter = entityPermissionFilter;
        EntityIdPermissionFilter = entityIdPermissionFilter;
        PropertyPermissionFilter = propertyPermissionFilter;
        ActionPermissionFilter = actionPermissionFilter;
    }
}