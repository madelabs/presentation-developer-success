using BBQ.Application.DTOs;
using BBQ.Application.DTOs.BbqSession;
using BBQ.Application.DTOs.SessionNote;
using BBQ.Application.DTOs.User;

namespace BBQ.Application.Services;

public interface IBbqService
{
    Task<CreateBbqSessionResponseDto> CreateAsync(CreateBbqSessionInputDto createBbqSessionInputDto);

    Task<BaseResponseDto> DeleteAsync(Guid id);

    Task<IEnumerable<BbqSessionResponseDto>> GetAllAsync();

    Task<UpdateBbqSessionResponseDto> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto);

    Task<CreateSessionNoteResponseDto> CreateAsync(CreateSessionNoteInputDto createSessionNoteInputDto,
    CancellationToken cancellationToken = default);

    Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<SessionNoteResponseDto>>
        GetAllByBbqSessionIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<UpdateSessionNoteResponseDto> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto,
        CancellationToken cancellationToken = default);

    Task<BaseResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordInputDto changePasswordInputDto);

    Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailInputDto confirmEmailInputDto);

    Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);

    Task<LoginResponseDto> LoginAsync(LoginUserInputDto loginUserInputDto);
}
