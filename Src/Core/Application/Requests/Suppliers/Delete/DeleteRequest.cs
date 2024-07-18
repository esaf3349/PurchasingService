using MediatR;

namespace Application.Requests.Suppliers.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
}