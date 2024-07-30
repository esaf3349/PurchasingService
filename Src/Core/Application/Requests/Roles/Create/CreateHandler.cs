using Application.Contracts.Infra.Persistence;
using Domain.Common.Guids;
using Domain.Model.Roles;
using MediatR;

namespace Application.Requests.Roles.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var newRole = new Role(AppGuid.New, request.Name, request.Description, request.ForSingleUser);

        _uow.Roles.Add(newRole);

        await _uow.SaveChangesAsync(cancellationToken);

        return newRole.Id;
    }
}