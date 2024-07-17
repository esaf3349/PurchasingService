using Application.Contracts.Infra.Persistence;
using Application.Contracts.Presentation.CurrentUser;
using Application.Contracts.Presentation.CurrentUser.Dtos;
using AutoMapper;
using WebApi.Services.CurrentUser.Constants;
using WebApi.Services.CurrentUser.Extensions;

namespace WebApi.Services.CurrentUser;

internal sealed class CurrentUserService : ICurrentUserService
{
    public User? Details { get; private set; }

    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;

    public CurrentUserService(IUnitOfWork uow, IMapper mapper, IHttpContextAccessor httpAccessor)
    {
        _uow = uow;
        _mapper = mapper;
        _httpContext = httpAccessor.HttpContext;

        if (!IsAuthenticated())
            return;

        InitUser();
    }

    private void InitUser()
    {
        var userSessionExists = _httpContext.Session.Keys.Contains(SessionConstants.CurrentUserSessionName);
        var user = userSessionExists ?
            _httpContext.Session.GetUser() :
            GetUserFromPersistence();

        Details = user;

        if (!userSessionExists)
            _httpContext.Session.SetUser(user);
    }

    private User? GetUserFromPersistence()
    {
        var accountName = _httpContext.User.Identity.GetAccountName();

        var persistedUser = _uow.Users.FirstOrDefault(u => u.Login == accountName && u.IsActive);

        var user = _mapper.Map<User>(persistedUser);

        return user;
    }

    private bool IsAuthenticated() => !string.IsNullOrWhiteSpace(_httpContext.User?.Identity?.Name);
}