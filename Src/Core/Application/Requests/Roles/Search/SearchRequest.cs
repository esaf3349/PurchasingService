using Domain.Model.Roles;
using MediatR;

namespace Application.Requests.Roles.Search;

public sealed record SearchRequest : IRequest<IEnumerable<Role>>
{
    public required string? Name { get; init; }
    public required string? Description { get; init; }
    public required bool? ForSingleUser { get; init; }
}