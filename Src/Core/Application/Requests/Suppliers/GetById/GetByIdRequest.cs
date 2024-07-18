using Domain.Model.Suppliers;
using MediatR;

namespace Application.Requests.Suppliers.GetById;

public sealed record GetByIdRequest : IRequest<Supplier>
{
    public required Guid Id { get; init; }
}