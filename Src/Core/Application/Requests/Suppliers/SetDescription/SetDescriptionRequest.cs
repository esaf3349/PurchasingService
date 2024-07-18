using MediatR;

namespace Application.Requests.Suppliers.SetDescription;

public sealed record SetDescriptionRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required string? Description { get; init; }
}