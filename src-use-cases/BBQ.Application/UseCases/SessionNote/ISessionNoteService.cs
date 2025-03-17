using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.SessionNote.CreateSessionNote;
using BBQ.Application.UseCases.SessionNote.GetAllByList;
using BBQ.Application.UseCases.SessionNote.UpdateSessionNote;

namespace BBQ.Application.UseCases.SessionNote;

public interface ISessionNoteService
{
    Task<CreateSessionNoteResponseDto> CreateAsync(CreateSessionNoteInputDto createSessionNoteInputDto,
        CancellationToken cancellationToken = default);

    Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<SessionNoteResponseDto>>
        GetAllByBbqSessionIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<UpdateSessionNoteResponseDto> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto,
        CancellationToken cancellationToken = default);
}
