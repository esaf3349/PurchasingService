using Domain.Model.RolePermissions.ValueObjects;
using MediatR;

namespace Application.Requests.Permissions.Create;

public sealed class CreateRequest : IRequest<Guid>
{
    public required Guid RoleId { get; init; }
    public required AllowedEntity? EntityPermissionFilter { get; init; }
    public required string? EntityIdPermissionFilter { get; init; }
    public required string? PropertyPermissionFilter { get; init; }
    public required AllowedAction? ActionPermissionFilter { get; init; }
}