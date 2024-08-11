using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Common.Guids;
using Domain.Model.Permissions;
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

        var newPermission = new Permission(AppGuid.New, request.EntityFilter, request.EntityIdFilter, request.PropertyFilter, request.ActionFilter);

        persistedRole.AddPermission(newPermission);
        _uow.Permissions.Add(newPermission);

        await _uow.SaveChangesAsync(cancellationToken);

        return newPermission.Id;
    }
}