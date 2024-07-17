using AutoMapper;
using Domain.Model.Users;
using CurrentUserDtos = Application.Contracts.Presentation.CurrentUser.Dtos;

namespace WebApi.MappingProfiles;

public sealed class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, CurrentUserDtos.User>();
    }
}