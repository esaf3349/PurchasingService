using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.SetSupplier;

public sealed class SetSupplierHandler : IRequestHandler<SetSupplierRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetSupplierHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetSupplierRequest request, CancellationToken cancellationToken = default)
    {
        var persistedSupplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.SupplierId && s.IsActive);
        if (persistedSupplier == null)
            throw new NotFoundException($"Supplier {request.SupplierId} doesn't exist");

        var persistedRequisition = await _uow.Requisitions.FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);
        if (persistedRequisition == null)
            throw new NotFoundException($"Requisition {request.Id} doesn't exist");

        persistedRequisition.SetSupplier(request.SupplierId);

        _uow.Requisitions.Update(persistedRequisition);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}