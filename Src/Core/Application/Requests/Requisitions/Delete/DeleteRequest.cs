using MediatR;

namespace Application.Requests.Requisitions.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
}