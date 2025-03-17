using MediatR;
using BBQ.Application.Common.DTO;
using BBQ.DataAccess.Repositories;

namespace BBQ.Application.UseCases.SessionNote.DeleteSessionNote;

public record DeleteSessionNoteCommand(
    Guid Id) : IRequest<BaseResponseDto>;

public class DeleteSessionNoteCommandHandler : IRequestHandler<DeleteSessionNoteCommand, BaseResponseDto>
{
    private readonly ISessionNoteRepository _sessionNoteRepository;

    public DeleteSessionNoteCommandHandler(ISessionNoteRepository sessionNoteRepository)
    {
        _sessionNoteRepository = sessionNoteRepository;
    }


    public async Task<BaseResponseDto> Handle(DeleteSessionNoteCommand request,
        CancellationToken cancellationToken = default)
    {
        var sessionNote = await _sessionNoteRepository.GetFirstAsync(ti => ti.Id == request.Id);

        return new BaseResponseDto
        {
            Id = (await _sessionNoteRepository.DeleteAsync(sessionNote)).Id
        };
    }
}
