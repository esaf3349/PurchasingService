using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Requisitions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Requisitions.Create;

public sealed class CreateHandler : IRequestHandler<CreateRequest, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreateHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var persistedSupplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.SupplierId && s.IsActive);
        if (persistedSupplier == null)
            throw new NotFoundException($"Supplier {request.SupplierId} doesn't exist");

        var persistedDepartment = await _uow.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId && d.IsActive);
        if (persistedDepartment == null)
            throw new NotFoundException($"Department {request.DepartmentId} doesn't exist");

        var newRequisition = new Requisition(Guid.NewGuid(), request.Title, request.SupplierId, request.DepartmentId, request.DeliveryDueDate);

        _uow.Requisitions.Add(newRequisition);

        await _uow.SaveChangesAsync();

        return newRequisition.Id;
    }
}