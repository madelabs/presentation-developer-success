using AutoMapper;
using BBQ.Application.DTOs.BbqSession;
using BBQ.DataAccess.Entities;

namespace BBQ.Application.MappingProfiles;

public class BbqSessionProfile : Profile
{
    public BbqSessionProfile()
    {
        CreateMap<CreateBbqSessionInputDto, BbqSession>();

        CreateMap<BbqSession, BbqSessionResponseDto>();
    }
}
