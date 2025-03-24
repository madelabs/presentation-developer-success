using AutoMapper;
using BBQ.Application.Common.Automapper;
using BBQ.Application.UseCases.SessionNote.CreateSessionNote;
using BBQ.Application.UseCases.SessionNote.GetAllByList;
using BBQ.Application.UseCases.SessionNote.UpdateSessionNote;

namespace BBQ.Application.UseCases.SessionNote;

public class SessionNoteProfile : Profile, IMappingProfilesMarker
{
    public SessionNoteProfile()
    {
        CreateMap<DataAccess.Entities.SessionNote, SessionNoteResponseDto>();
    }
}
