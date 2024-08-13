using MediatR;

namespace Application.Requests.UserRoles.Create;

public sealed record CreateRequest : IRequest<Unit>
{
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
}