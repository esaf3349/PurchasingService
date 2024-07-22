﻿using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Departments.SetName;

public sealed class SetNameHandler : IRequestHandler<SetNameRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetNameHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetNameRequest request, CancellationToken cancellationToken = default)
    {
        var persistedDepartment = await _uow.Departments.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedDepartment == null)
            throw new NotFoundException($"Department {request.Id} doesn't exist");

        persistedDepartment.SetName(request.Name);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}