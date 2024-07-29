using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Common.Guids;
using Domain.Model.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users.FirstOrDefaultAsync(u => u.Login == request.Login && u.IsActive);
        if (persistedUser != null)
            throw new AlreadyExistsException($"User {request.Login} already exists");

        var newUser = new User(AppGuid.New, request.Login);

        _uow.Users.Add(newUser);

        await _uow.SaveChangesAsync();

        return Unit.Value;
    }
}