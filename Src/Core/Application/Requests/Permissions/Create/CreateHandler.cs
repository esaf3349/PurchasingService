using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Common.Guids;
using Domain.Model.RolePermissions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Permissions.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRole = await _uow.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId && r.IsActive, cancellationToken);

        if (persistedRole == null)
            throw new NotFoundException($"Role {request.RoleId} doesn't exist");

        var newPermission = new RolePermission(AppGuid.New, request.EntityPermissionFilter, request.EntityIdPermissionFilter, request.PropertyPermissionFilter, request.ActionPermissionFilter);

        persistedRole.AddPermission(newPermission);
        _uow.RolePermissions.Add(newPermission);

        await _uow.SaveChangesAsync(cancellationToken);

        return newPermission.Id;
    }
}