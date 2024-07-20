using MediatR;

namespace Application.Requests.Users.SetEmail;

public sealed record SetEmailRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
}