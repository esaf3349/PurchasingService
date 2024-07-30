using MediatR;

namespace Application.Requests.Roles.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
}