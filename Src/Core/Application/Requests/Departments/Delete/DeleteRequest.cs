using MediatR;

namespace Application.Requests.Departments.Delete;

public sealed record DeleteRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
}