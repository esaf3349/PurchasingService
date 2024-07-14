﻿using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.Create;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public CreateUserHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _uow.Users.FirstOrDefaultAsync(u => u.Login == request.Login && u.IsActive);
        if (existingUser != null)
            throw new AlreadyExistsException($"User {request.Login} already exists");

        var user = new User(Guid.NewGuid(), request.Login);

        _uow.Users.Add(user);

        await _uow.SaveChangesAsync();

        return Unit.Value;
    }
}