using MediatR;

namespace Application.Requests.Requisitions.Create;

public sealed record CreateRequest : IRequest<Guid>
{
    public required string Title { get; init; }
    public required Guid SupplierId { get; init; }
    public required Guid DepartmentId { get; init; }
    public required DateTime DeliveryDueDate { get; init; }
}