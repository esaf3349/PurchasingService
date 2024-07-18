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
        var supplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (supplier == null)
            throw new NotFoundException($"Supplier {request.Id} doesn't exist");

        supplier.Delete();

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}