using MediatR;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.ValueObjects;

namespace BBQ.Application.UseCases.SessionNote.UpdateSessionItem;

public record UpdateSessionNoteCommand(
    Guid Id,
    string ActivityDescription,
    string Note,
    PitTemperature PitTemperature,
    MeatTemperature MeatTemperature) : IRequest<UpdateSessionNoteResponseDto>;

public class UpdateSessionNoteCommandHandler : IRequestHandler<UpdateSessionNoteCommand, UpdateSessionNoteResponseDto>
{
    private readonly ISessionNoteRepository _sessionNoteRepository;

    public UpdateSessionNoteCommandHandler(ISessionNoteRepository sessionNoteRepository)
    {
        _sessionNoteRepository = sessionNoteRepository;
    }


    public async Task<UpdateSessionNoteResponseDto> Handle(UpdateSessionNoteCommand request,
        CancellationToken cancellationToken = default)
    {
        var sessionNote = await _sessionNoteRepository.GetFirstAsync(ti => ti.Id == request.Id);

        sessionNote.ActivityDescription = request.ActivityDescription;
        sessionNote.Note = request.Note;
        sessionNote.PitTemperature = request.PitTemperature;
        sessionNote.MeatTemperature = request.MeatTemperature;

        return new UpdateSessionNoteResponseDto
        {
            Id = (await _sessionNoteRepository.UpdateAsync(sessionNote)).Id
        };
    }
}
