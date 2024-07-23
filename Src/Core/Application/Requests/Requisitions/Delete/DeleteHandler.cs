using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.Delete;

public sealed class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRequisition = await _uow.Requisitions.FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);
        if (persistedRequisition == null)
            throw new NotFoundException($"Requisition {request.Id} doesn't exist");

        persistedRequisition.Delete();

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}