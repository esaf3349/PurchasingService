using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Roles.GetById;

public sealed class GetByIdHandler : IRequestHandler<GetByIdRequest, Role>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Role> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRole = await _uow.Roles
            .Include(r => r.Permissions.Where(p => p.IsActive))
            .FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);

        if (persistedRole == null)
            throw new NotFoundException($"Role {request.Id} doesn't exist");

        return persistedRole;
    }
}