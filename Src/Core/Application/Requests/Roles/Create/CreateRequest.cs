using MediatR;

namespace Application.Requests.Roles.Create;

public sealed record CreateRequest : IRequest<Guid>
{
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required bool ForSingleUser { get; init; }
}