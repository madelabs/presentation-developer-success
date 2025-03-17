using AutoMapper;
using BBQ.Application.Common.Automapper;
using BBQ.Application.UseCases.User.CreateUser;
using BBQ.DataAccess.Identity;

namespace BBQ.Application.UseCases.User;

public class UserProfile : Profile, IMappingProfilesMarker
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, ApplicationUser>();
    }
}
