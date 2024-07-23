using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.SetDepartment;

public sealed class SetDepartmentHandler : IRequestHandler<SetDepartmentRequest, Unit>
{
    private readonly IUnitOfWork _uow;

    public SetDepartmentHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(SetDepartmentRequest request, CancellationToken cancellationToken = default)
    {
        var persistedDepartment = await _uow.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId && d.IsActive);
        if (persistedDepartment == null)
            throw new NotFoundException($"Department {request.DepartmentId} doesn't exist");

        var persistedRequisition = await _uow.Requisitions.FirstOrDefaultAsync(r => r.Id == request.Id && r.IsActive);
        if (persistedRequisition == null)
            throw new NotFoundException($"Requisition {request.Id} doesn't exist");

        persistedRequisition.SetDepartment(request.DepartmentId);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}