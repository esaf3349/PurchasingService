using Application.Contracts.Infra.Persistence;
using Domain.Model.Suppliers;
using MediatR;

namespace Application.Requests.Suppliers.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var newSupplier = new Supplier(Guid.NewGuid(), request.Name, request.Description);

        _uow.Suppliers.Add(newSupplier);

        await _uow.SaveChangesAsync(cancellationToken);

        return newSupplier.Id;
    }
}