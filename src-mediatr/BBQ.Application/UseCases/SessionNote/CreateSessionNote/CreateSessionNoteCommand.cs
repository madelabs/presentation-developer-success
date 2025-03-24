using MediatR;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.ValueObjects;

namespace BBQ.Application.UseCases.SessionNote.CreateSessionNote;

public record CreateSessionNoteCommand(
    Guid BbqSessionId,
    string ActivityDescription,
    string Note,
    PitTemperature PitTemperature,
    MeatTemperature MeatTemperature) : IRequest<CreateSessionNoteResponseDto>;

public class CreateSessionNoteCommandHandler : IRequestHandler<CreateSessionNoteCommand, CreateSessionNoteResponseDto>
{
    private readonly ISessionNoteRepository _sessionNoteRepository;
    private readonly IBbqSessionRepository _bbqSessionRepository;

    public CreateSessionNoteCommandHandler(ISessionNoteRepository sessionNoteRepository,
        IBbqSessionRepository bbqSessionRepository)
    {
        _sessionNoteRepository = sessionNoteRepository;
        _bbqSessionRepository = bbqSessionRepository;
    }


    public async Task<CreateSessionNoteResponseDto> Handle(CreateSessionNoteCommand request,
        CancellationToken cancellationToken = default)
    {
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == request.BbqSessionId);
        var sessionNote = new DataAccess.Entities.SessionNote()
        {
            Session = bbqSession,
            ActivityDescription = request.ActivityDescription,
            Note = request.Note,
            PitTemperature = request.PitTemperature,
            MeatTemperature = request.MeatTemperature
        };

        return new CreateSessionNoteResponseDto
        {
            Id = (await _sessionNoteRepository.AddAsync(sessionNote)).Id
        };
    }
}
