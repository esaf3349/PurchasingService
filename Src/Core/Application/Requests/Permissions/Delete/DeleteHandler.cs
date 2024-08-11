using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Permissions.Delete;

public sealed class DeletePermissionHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeletePermissionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRole = await _uow.Roles
            .Include(r => r.Permissions.Where(p => p.IsActive))
            .FirstOrDefaultAsync(r => r.Id == request.RoleId && r.IsActive, cancellationToken);

        if (persistedRole == null)
            throw new NotFoundException($"Role {request.RoleId} doesn't exist");

        if (!persistedRole.Permissions.Any(p => p.Id == request.PermissionId))
            throw new NotFoundException($"Permission {request.PermissionId} doesn't exist");

        persistedRole.RemovePermission(request.PermissionId);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}