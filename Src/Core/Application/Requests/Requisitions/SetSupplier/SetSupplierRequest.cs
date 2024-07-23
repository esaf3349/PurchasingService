using MediatR;

namespace Application.Requests.Requisitions.SetSupplier;

public sealed record SetSupplierRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required Guid SupplierId { get; init; }
}