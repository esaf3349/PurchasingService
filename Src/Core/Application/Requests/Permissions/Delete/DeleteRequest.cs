using MediatR;

namespace Application.Requests.Permissions.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid RoleId { get; init; }
    public required Guid PermissionId { get; init; }
}