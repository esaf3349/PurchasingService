using MediatR;

namespace Application.Requests.Users.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
}