using Domain.Model.Departments;
using MediatR;

namespace Application.Requests.Departments.GetById;

public sealed record GetByIdRequest : IRequest<Department>
{
    public required Guid Id { get; init; }
}