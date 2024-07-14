using MediatR;

namespace Application.Requests.Users.Create;

public sealed record CreateUserRequest : IRequest<Unit>
{
    public required string Login { get; init; }
}