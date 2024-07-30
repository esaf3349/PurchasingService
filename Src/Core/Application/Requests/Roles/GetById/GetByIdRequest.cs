using Domain.Model.Roles;
using MediatR;

namespace Application.Requests.Roles.GetById;

public sealed record GetByIdRequest : IRequest<Role>
{
    public required Guid Id { get; init; }
}