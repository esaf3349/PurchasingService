using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Suppliers.SetName;

public sealed class SetNameHandler : IRequestHandler<SetNameRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetNameHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetNameRequest request, CancellationToken cancellationToken = default)
    {
        var supplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (supplier == null)
            throw new NotFoundException($"Supplier {request.Id} doesn't exist");

        supplier.SetName(request.Name);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}