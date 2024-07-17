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
        var supplier = await _uow.Suppliers.FirstOrDefaultAsync(s => s.Id == request.SupplierId && s.IsActive);
        if (supplier == null)
            throw new NotFoundException($"Supplier {request.SupplierId} doesn't exist");

        var department = await _uow.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId && d.IsActive);
        if (department == null)
            throw new NotFoundException($"Department {request.DepartmentId} doesn't exist");

        var requisition = new Requisition(Guid.NewGuid(), request.Title, request.SupplierId, request.DepartmentId, request.DeliveryDueDate);

        _uow.Requisitions.Add(requisition);

        await _uow.SaveChangesAsync();

        return requisition.Id;
    }
}