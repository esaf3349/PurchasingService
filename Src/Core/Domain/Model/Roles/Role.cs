﻿using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Permissions;
using Domain.Model.Roles.Constants;
using Domain.Model.UserRoles;

namespace Domain.Model.Roles;

public sealed class Role : BaseEntity<Guid>
{
    private readonly HashSet<Permission> _permissions = [];
    private readonly HashSet<UserRole> _userRoles = [];

    private Role() { }

    public Role(Guid id, string name, string? description, bool forSingleUser) : base(id)
    {
        SetName(name);
        SetDescription(description);

        ForSingleUser = forSingleUser;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool ForSingleUser { get; private init; }

    public IReadOnlyCollection<Permission> Permissions => _permissions;
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

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

    public void AddPermission(Permission permission)
    {
        _permissions.Add(permission);
    }

    public void RemovePermission(Guid permissionId)
    {
        var permission = _permissions.FirstOrDefault(p => p.Id == permissionId);
        if (permission == null)
            throw new DomainException<Role>($"Permission {permissionId} doesn't exist");

        permission.Delete();
    }
}