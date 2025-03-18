using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.User;
using BBQ.Application.Services;

namespace BBQ.API.Controllers;

public class UsersController : ApiController
{
    private readonly IBbqService _bbqService;

    public UsersController(IBbqService userService)
    {
        _bbqService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserDto createUserDto)
    {
        return Ok(ApiResult<CreateUserResponseDto>.Success(await _bbqService.CreateAsync(createUserDto)));
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginUserInputDto loginUserInputDto)
    {
        return Ok(ApiResult<LoginResponseDto>.Success(await _bbqService.LoginAsync(loginUserInputDto)));
    }

    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailInputDto confirmEmailInputDto)
    {
        return Ok(ApiResult<ConfirmEmailResponseDto>.Success(
            await _bbqService.ConfirmEmailAsync(confirmEmailInputDto)));
    }

    [HttpPut("{id:guid}/changePassword")]
    public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordInputDto changePasswordInputDto)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(
            await _bbqService.ChangePasswordAsync(id, changePasswordInputDto)));
    }
}
