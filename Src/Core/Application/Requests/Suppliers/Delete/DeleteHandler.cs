using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Suppliers.Delete;

public sealed class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedSupplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedSupplier == null)
            throw new NotFoundException($"Supplier {request.Id} doesn't exist");

        persistedSupplier.Delete();

        _uow.Suppliers.Update(persistedSupplier);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}