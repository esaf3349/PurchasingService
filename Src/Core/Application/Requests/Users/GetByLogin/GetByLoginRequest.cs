using Domain.Model.Users;
using MediatR;

namespace Application.Requests.Users.GetByLogin;

public sealed record GetByLoginRequest : IRequest<User>
{
    public required string Login { get; init; }
}