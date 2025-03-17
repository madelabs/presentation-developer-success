using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.User;
using BBQ.Application.Services;

namespace BBQ.API.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserDto createUserDto)
    {
        return Ok(ApiResult<CreateUserResponseDto>.Success(await _userService.CreateAsync(createUserDto)));
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginUserInputDto loginUserInputDto)
    {
        return Ok(ApiResult<LoginResponseDto>.Success(await _userService.LoginAsync(loginUserInputDto)));
    }

    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailInputDto confirmEmailInputDto)
    {
        return Ok(ApiResult<ConfirmEmailResponseDto>.Success(
            await _userService.ConfirmEmailAsync(confirmEmailInputDto)));
    }

    [HttpPut("{id:guid}/changePassword")]
    public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordInputDto changePasswordInputDto)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(
            await _userService.ChangePasswordAsync(id, changePasswordInputDto)));
    }
}
