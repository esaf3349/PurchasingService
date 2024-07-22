using MediatR;

namespace Application.Requests.Departments.Create;

public sealed record CreateRequest : IRequest<Guid>
{
    public required string Name { get; init; }
}