using Domain.Model.RolePermissions.ValueObjects;

namespace WebApi.Controllers.Permissions.Dtos;

public sealed record AddRequest(AllowedEntity? EntityPermissionFilter, string? EntityIdPermissionFilter, string? PropertyPermissionFilter, AllowedAction? ActionPermissionFilter);