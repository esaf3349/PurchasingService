﻿using Application.Contracts.Infra.Persistence;
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
        var persistedSupplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedSupplier == null)
            throw new NotFoundException($"Supplier {request.Id} doesn't exist");

        persistedSupplier.SetName(request.Name);

        _uow.Suppliers.Update(persistedSupplier);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}