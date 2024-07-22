using Domain.Model.Departments;
using MediatR;

namespace Application.Requests.Departments.Search;

public sealed record SearchRequest : IRequest<IEnumerable<Department>>
{
    public required string? Name { get; init; }
}