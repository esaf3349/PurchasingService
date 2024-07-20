using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.SetEmail;

public sealed class SetEmailHandler : IRequestHandler<SetEmailRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetEmailHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetEmailRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedUser == null)
            throw new NotFoundException($"User {request.Id} doesn't exist");

        persistedUser.SetEmail(request.Email);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}