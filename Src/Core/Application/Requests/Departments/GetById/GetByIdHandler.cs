using Application.Contracts.Infra.Persistence;
using Application.Exceptions;
using Domain.Model.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Departments.GetById;

public sealed class GetByIdHandler : IRequestHandler<GetByIdRequest, Department>
{
    private readonly IUnitOfWork _uow;

    public GetByIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Department> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var persistedDepartment = await _uow.Departments.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsActive);
        if (persistedDepartment == null)
            throw new NotFoundException($"Department {request.Id} doesn't exist");

        return persistedDepartment;
    }
}