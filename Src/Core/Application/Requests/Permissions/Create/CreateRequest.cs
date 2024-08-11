using Domain.Model.Permissions.ValueObjects;
using MediatR;

namespace Application.Requests.Permissions.Create;

public sealed class CreateRequest : IRequest<Guid>
{
    public required Guid RoleId { get; init; }
    public required AllowedEntity? EntityFilter { get; init; }
    public required string? EntityIdFilter { get; init; }
    public required string? PropertyFilter { get; init; }
    public required AllowedAction? ActionFilter { get; init; }
}