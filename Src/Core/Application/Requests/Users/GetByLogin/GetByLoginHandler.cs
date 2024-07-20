using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.GetByLogin;

public sealed class GetByIdHandler : IRequestHandler<GetByLoginRequest, User>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<User> Handle(GetByLoginRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users.FirstOrDefaultAsync(u => u.Login == request.Login && u.IsActive);
        if (persistedUser == null)
            throw new NotFoundException($"User {request.Login} doesn't exist");

        return persistedUser;
    }
}