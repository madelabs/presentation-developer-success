using BBQ.Application.DTOs;
using BBQ.Application.DTOs.User;

namespace BBQ.Application.Services;

public interface IUserService
{
    Task<BaseResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordInputDto changePasswordInputDto);

    Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailInputDto confirmEmailInputDto);

    Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);

    Task<LoginResponseDto> LoginAsync(LoginUserInputDto loginUserInputDto);
}
