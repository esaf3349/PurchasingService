using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.UserRoles.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users
            .Include(u => u.UserRoles.Where(r => r.IsActive))
            .FirstOrDefaultAsync(u => u.Id == request.UserId && u.IsActive);

        if (persistedUser == null)
            throw new NotFoundException($"User {request.UserId} doesn't exist");

        var persistedRole = await _uow.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId && r.IsActive);
        if (persistedRole == null)
            throw new NotFoundException($"Role {request.RoleId} doesn't exist");

        var newUserRole = persistedUser.AddRole(persistedRole);

        await _uow.UserRoles.AddAsync(newUserRole, cancellationToken);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}