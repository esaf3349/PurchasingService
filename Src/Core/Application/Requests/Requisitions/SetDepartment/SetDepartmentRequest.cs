using MediatR;

namespace Application.Requests.Requisitions.SetDepartment;

public sealed record SetDepartmentRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required Guid DepartmentId { get; init; }
}