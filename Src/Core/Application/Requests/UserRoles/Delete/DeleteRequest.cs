using MediatR;

namespace Application.Requests.UserRoles.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
}