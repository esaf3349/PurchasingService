using MediatR;

namespace Application.Requests.Users.Create;

public sealed record CreateRequest : IRequest<Unit>
{
    public required string Login { get; init; }
}