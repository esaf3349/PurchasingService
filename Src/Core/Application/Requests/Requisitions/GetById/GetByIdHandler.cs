using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Requisitions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.GetById;

public sealed class GetByIdHandler : IRequestHandler<GetByIdRequest, Requisition>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Requisition> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var requisition = await _uow.Requisitions.FirstOrDefaultAsync(u => u.Id == request.Id && u.IsActive);
        if (requisition == null)
            throw new NotFoundException($"Requisition {request.Id} doesn't exist");

        return requisition;
    }
}