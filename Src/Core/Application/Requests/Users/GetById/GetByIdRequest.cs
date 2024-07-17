using Domain.Model.Users;
using MediatR;

namespace Application.Requests.Users.GetById;

public sealed record GetByIdRequest : IRequest<User>
{
    public required Guid Id { get; init; }
}