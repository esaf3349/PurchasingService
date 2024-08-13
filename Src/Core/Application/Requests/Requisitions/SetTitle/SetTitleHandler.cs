using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.SetTitle;

public sealed class SetTitleHandler : IRequestHandler<SetTitleRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetTitleHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetTitleRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRequisition = await _uow.Requisitions.FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);
        if (persistedRequisition == null)
            throw new NotFoundException($"Requisition {request.Id} doesn't exist");

        persistedRequisition.SetTitle(request.Title);

        _uow.Requisitions.Update(persistedRequisition);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}