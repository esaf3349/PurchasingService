using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Departments.Delete;

public sealed class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public DeleteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var persistedDepartment = await _uow.Departments.FirstOrDefaultAsync(d => d.Id == request.Id && d.IsActive);
        if (persistedDepartment == null)
            throw new NotFoundException($"Department {request.Id} doesn't exist");

        persistedDepartment.Delete();

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}