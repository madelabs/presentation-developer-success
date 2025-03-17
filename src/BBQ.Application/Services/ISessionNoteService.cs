using BBQ.Application.DTOs;
using BBQ.Application.DTOs.SessionNote;

namespace BBQ.Application.Services;

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
