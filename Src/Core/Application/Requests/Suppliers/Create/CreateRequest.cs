using MediatR;

namespace Application.Requests.Suppliers.Create;

public sealed record CreateRequest : IRequest<Guid>
{
    public required string Name { get; init; }
    public required string? Description { get; init; }
}