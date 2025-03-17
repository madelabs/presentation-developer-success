using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BBQ.Application.Common.DTO;
using BBQ.Application.Common.Email;
using BBQ.Application.Common.Exceptions;
using BBQ.Application.Common.Helpers;
using BBQ.Application.Common.Services;
using BBQ.Application.Templates;
using BBQ.Application.UseCases.User.ChangePassword;
using BBQ.Application.UseCases.User.ConfirmEmail;
using BBQ.Application.UseCases.User.CreateUser;
using BBQ.Application.UseCases.User.LoginUser;
using BBQ.DataAccess.Identity;

namespace BBQ.Application.UseCases.User;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITemplateService _templateService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ITemplateService templateService,
        IEmailService emailService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _templateService = templateService;
        _emailService = emailService;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<ApplicationUser>(createUserDto);

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var emailTemplate = await _templateService.GetTemplateAsync(TemplateConstants.ConfirmationEmail);

        var emailBody = _templateService.ReplaceInTemplate(emailTemplate,
            new Dictionary<string, string> { { "{UserId}", user.Id }, { "{Token}", token } });

        await _emailService.SendEmailAsync(EmailMessage.Create(user.Email, emailBody, "[N-Tier]Confirm your email"));

        return new CreateUserResponseDto
        {
            Id = Guid.Parse((await _userManager.FindByNameAsync(user.UserName)).Id)
        };
    }

    public async Task<LoginResponseDto> LoginAsync(LoginUserInputDto loginUserInputDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginUserInputDto.Username);

        if (user == null)
            throw new NotFoundException("Username or password is incorrect");

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserInputDto.Password, false, false);

        if (!signInResult.Succeeded)
            throw new BadRequestException("Username or password is incorrect");

        var token = JwtHelper.GenerateToken(user, _configuration);

        return new LoginResponseDto
        {
            Username = user.UserName,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailInputDto confirmEmailInputDto)
    {
        var user = await _userManager.FindByIdAsync(confirmEmailInputDto.UserId);

        if (user == null)
            throw new UnprocessableRequestException("Your verification link is incorrect");

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailInputDto.Token);

        if (!result.Succeeded)
            throw new UnprocessableRequestException("Your verification link has expired");

        return new ConfirmEmailResponseDto
        {
            Confirmed = true
        };
    }

    public async Task<BaseResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordInputDto changePasswordInputDto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        var result =
            await _userManager.ChangePasswordAsync(user, changePasswordInputDto.OldPassword,
                changePasswordInputDto.NewPassword);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        return new BaseResponseDto
        {
            Id = Guid.Parse(user.Id)
        };
    }
}
