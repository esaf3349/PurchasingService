using Application.Contracts.Infra.Persistence;
using Application.Contracts.Presentation.CurrentUser;
using Application.Exceptions;
using Domain.Common.Guids;
using Domain.Model.Requisitions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUser;

    public CreateHandler(IUnitOfWork uow, ICurrentUserService currentUser)
    {
        _uow = uow;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var persistedSupplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.SupplierId && s.IsActive);
        if (persistedSupplier == null)
            throw new NotFoundException($"Supplier {request.SupplierId} doesn't exist");

        var persistedDepartment = await _uow.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId && d.IsActive);
        if (persistedDepartment == null)
            throw new NotFoundException($"Department {request.DepartmentId} doesn't exist");

        var newRequisition = new Requisition(AppGuid.New, request.Title, request.SupplierId, request.DepartmentId, _currentUser.Details.Id, request.DueDate);

        await _uow.Requisitions.AddAsync(newRequisition, cancellationToken);

        await _uow.SaveChangesAsync(cancellationToken);

        return newRequisition.Id;
    }
}