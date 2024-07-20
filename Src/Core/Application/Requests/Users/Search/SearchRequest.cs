using Domain.Model.Users;
using MediatR;

namespace Application.Requests.Users.Search;

public sealed record SearchRequest : IRequest<IEnumerable<User>>
{
    public required string? Login { get; init; }
    public required string? FirstName { get; init; }
    public required string? LastName { get; init; }
    public required string? MiddleName { get; init; }
    public required string? Email { get; init; }
}