using AutoMapper;
using BBQ.Application.DTOs.User;
using BBQ.DataAccess.Identity;

namespace BBQ.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, ApplicationUser>();
    }
}
