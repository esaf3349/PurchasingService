﻿using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Requisitions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.GetById;

public sealed class GetByIdHandler : IRequestHandler<GetByIdRequest, Requisition>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Requisition> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRequisition = await _uow.Requisitions.FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);
        if (persistedRequisition == null)
            throw new NotFoundException($"Requisition {request.Id} doesn't exist");

        return persistedRequisition;
    }
}