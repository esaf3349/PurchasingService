using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.GetById;

public sealed class GetByIdHandler : IRequestHandler<GetByIdRequest, User>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<User> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users.FirstOrDefaultAsync(u => u.Id == request.Id && u.IsActive);
        if (persistedUser == null)
            throw new NotFoundException($"User {request.Id} doesn't exist");

        return persistedUser;
    }
}