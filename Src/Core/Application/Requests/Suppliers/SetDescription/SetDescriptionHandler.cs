using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Suppliers.SetDescription;

public sealed class SetDescriptionHandler : IRequestHandler<SetDescriptionRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetDescriptionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetDescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var supplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (supplier == null)
            throw new NotFoundException($"Supplier {request.Id} doesn't exist");

        supplier.SetDescription(request.Description);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}