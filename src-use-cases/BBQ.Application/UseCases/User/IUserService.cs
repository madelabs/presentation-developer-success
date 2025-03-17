using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.User.ChangePassword;
using BBQ.Application.UseCases.User.ConfirmEmail;
using BBQ.Application.UseCases.User.CreateUser;
using BBQ.Application.UseCases.User.LoginUser;

namespace BBQ.Application.UseCases.User;

public interface IUserService
{
    Task<BaseResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordInputDto changePasswordInputDto);

    Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailInputDto confirmEmailInputDto);

    Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);

    Task<LoginResponseDto> LoginAsync(LoginUserInputDto loginUserInputDto);
}
