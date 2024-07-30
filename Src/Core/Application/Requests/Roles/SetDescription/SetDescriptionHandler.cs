using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Roles.SetDescription;

public sealed class SetDescriptionHandler : IRequestHandler<SetDescriptionRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetDescriptionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetDescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var persistedRole = await _uow.Roles.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedRole == null)
            throw new NotFoundException($"Role {request.Id} doesn't exist");

        persistedRole.SetDescription(request.Description);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}