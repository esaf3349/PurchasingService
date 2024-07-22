﻿using Application.Contracts.Infra.Persistence;
using Domain.Model.Departments;
using MediatR;

namespace Application.Requests.Departments.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var newDepartment = new Department(Guid.NewGuid(), request.Name);

        _uow.Departments.Add(newDepartment);

        await _uow.SaveChangesAsync(cancellationToken);

        return newDepartment.Id;
    }
}