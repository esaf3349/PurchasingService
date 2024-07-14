using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.Get;

public sealed class GetUserHandler : IRequestHandler<GetUserRequest, User>
{
    private readonly IUnitOfWork _uow;

    public GetUserHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _uow.Users.FirstOrDefaultAsync(u => u.Id == request.Id && u.IsActive);
        if (user == null)
            throw new NotFoundException($"User {request.Id} doesn't exists");

        return user;
    }
}