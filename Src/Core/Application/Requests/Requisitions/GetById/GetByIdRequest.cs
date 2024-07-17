using Domain.Model.Requisitions;
using MediatR;

namespace Application.Requests.Requisitions.GetById;

public sealed record GetByIdRequest : IRequest<Requisition>
{
    public required Guid Id { get; init; }
}