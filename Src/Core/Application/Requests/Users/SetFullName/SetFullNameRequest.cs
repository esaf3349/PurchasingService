using MediatR;

namespace Application.Requests.Users.SetFullName;

public sealed record SetFullNameRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string? MiddleName { get; init; }
}