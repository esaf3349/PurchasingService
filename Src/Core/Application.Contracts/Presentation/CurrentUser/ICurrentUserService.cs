using Application.Contracts.Presentation.CurrentUser.Dtos;

namespace Application.Contracts.Presentation.CurrentUser;

public interface ICurrentUserService
{
    User? Details { get; }
}