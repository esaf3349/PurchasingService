using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Suppliers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Suppliers.GetById;

public sealed class GetByIdHandler : IRequestHandler<GetByIdRequest, Supplier>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Supplier> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var supplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (supplier == null)
            throw new NotFoundException($"Supplier {request.Id} doesn't exist");

        return supplier;
    }
}