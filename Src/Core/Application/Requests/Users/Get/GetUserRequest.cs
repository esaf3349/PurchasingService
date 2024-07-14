using Domain.Model.Users;
using MediatR;

namespace Application.Requests.Users.Get;

public sealed record GetUserRequest : IRequest<User>
{
    public required Guid Id { get; init; }
}