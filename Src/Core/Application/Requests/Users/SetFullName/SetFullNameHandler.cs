using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Users.SetFullName;

public sealed class SetFullNameHandler : IRequestHandler<SetFullNameRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetFullNameHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetFullNameRequest request, CancellationToken cancellationToken = default)
    {
        var persistedUser = await _uow.Users.FirstOrDefaultAsync(u => u.Id == request.Id && u.IsActive);
        if (persistedUser == null)
            throw new NotFoundException($"User {request.Id} doesn't exist");

        persistedUser.SetFirstName(request.FirstName);
        persistedUser.SetLastName(request.LastName);
        persistedUser.SetMiddleName(request.MiddleName);

        _uow.Users.Update(persistedUser);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}