using AutoMapper;
using BBQ.Application.Common.Automapper;
using BBQ.Application.UseCases.BbqSession.CreateBbqSession;
using BBQ.Application.UseCases.BbqSession.GetAll;

namespace BBQ.Application.UseCases.BbqSession;

public class BbqSessionProfile : Profile, IMappingProfilesMarker
{
    public BbqSessionProfile()
    {
        CreateMap<DataAccess.Entities.BbqSession, BbqSessionResponseDto>();
    }
}
