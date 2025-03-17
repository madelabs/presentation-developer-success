using AutoMapper;
using BBQ.Application.DTOs.SessionNote;
using BBQ.DataAccess.Entities;

namespace BBQ.Application.MappingProfiles;

public class SessionNoteProfile : Profile
{
    public SessionNoteProfile()
    {
        CreateMap<CreateSessionNoteInputDto, SessionNote>();

        CreateMap<UpdateSessionNoteInputDto, SessionNote>();

        CreateMap<SessionNote, SessionNoteResponseDto>();
    }
}
