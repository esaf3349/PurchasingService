using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Roles.SetName;

public sealed class SetNameHandler : IRequestHandler<SetNameRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetNameHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetNameRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRole = await _uow.Roles.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedRole == null)
            throw new NotFoundException($"Role {request.Id} doesn't exist");

        persistedRole.SetName(request.Name);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}