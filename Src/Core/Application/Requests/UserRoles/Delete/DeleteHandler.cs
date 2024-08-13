using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.UserRoles.Delete;

public sealed class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users
            .Include(u => u.UserRoles.Where(r => r.IsActive))
            .FirstOrDefaultAsync(u => u.Id == request.UserId && u.IsActive, cancellationToken);

        if (persistedUser == null)
            throw new NotFoundException($"User {request.UserId} doesn't exist");

        persistedUser.RemoveRole(request.RoleId);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}