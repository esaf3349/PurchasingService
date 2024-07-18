using Domain.Model.Suppliers;
using MediatR;

namespace Application.Requests.Suppliers.Search;

public sealed record SearchRequest : IRequest<IEnumerable<Supplier>>
{
    public required string? Name { get; init; }
    public required string? Description { get; init; }
}