﻿using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.Delete;

public sealed class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users.FirstOrDefaultAsync(u => u.Id == request.Id && u.IsActive);
        if (persistedUser == null)
            throw new NotFoundException($"User {request.Id} doesn't exist");

        persistedUser.Delete();

        _uow.Users.Update(persistedUser);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}