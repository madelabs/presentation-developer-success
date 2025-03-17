using MediatR;
using BBQ.DataAccess.Repositories;

namespace BBQ.Application.UseCases.SessionNote.GetAllByList;

public record GetAllByBbqSessionQuery(Guid Id ) : IRequest<IEnumerable<SessionNoteResponseDto>>;

public class GetAllByListQueryHandler : IRequestHandler<GetAllByBbqSessionQuery, IEnumerable<SessionNoteResponseDto>>
{
    private readonly ISessionNoteRepository _sessionNoteRepository;

    public GetAllByListQueryHandler(ISessionNoteRepository sessionNoteRepository)
    {
        _sessionNoteRepository = sessionNoteRepository;
    }


    public async Task<IEnumerable<SessionNoteResponseDto>> Handle(GetAllByBbqSessionQuery request,
        CancellationToken cancellationToken = default)
    {
        var sessionNotes = await _sessionNoteRepository.GetAllAsync(ti => ti.Session.Id == request.Id);

        var result = new List<SessionNoteResponseDto>();
        foreach (var sessionNote in sessionNotes)
        {
            result.Add(new SessionNoteResponseDto()
            {
                Id = sessionNote.Id,
                ActivityDescription = sessionNote.ActivityDescription,
                Note = sessionNote.Note,
                PitTemperature = sessionNote.PitTemperature,
                CreatedBy = sessionNote.CreatedBy,
                CreatedOn = sessionNote.CreatedOn,
                UpdatedBy = sessionNote.UpdatedBy,
                UpdatedOn = sessionNote.UpdatedOn
            });
        }
        
        return result;
    }
}
