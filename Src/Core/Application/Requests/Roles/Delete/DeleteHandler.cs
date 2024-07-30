using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Roles.Delete;

public sealed class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRole = await _uow.Roles.FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);
        if (persistedRole == null)
            throw new NotFoundException($"Role {request.Id} doesn't exist");

        persistedRole.Delete();

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}