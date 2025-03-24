using AutoMapper;
using FluentValidation;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.BbqSession;
using BBQ.Application.Exceptions;
using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.Services;
using BBQ.Application.DTOs.SessionNote;
using BBQ.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BBQ.Application.Common.Email;
using BBQ.Application.DTOs.User;
using BBQ.Application.Helpers;
using BBQ.Application.Templates;
using Microsoft.EntityFrameworkCore;

namespace BBQ.Application.Services;

public class BbqService : IBbqService
{
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    private readonly IBbqSessionRepository _bbqSessionRepository;
    private readonly ISessionNotesRepository _sessionNotesRepository;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITemplateService _templateService;
    private readonly UserManager<ApplicationUser> _userManager;

    private const int MinimumDescriptionLength = 5;
    private const int MaximumDescriptionLength = 50;
    private const int MinimumNoteLength = 5;
    private const int MaximumNoteLength = 50;
    private const int MinimumActivityDescriptionLength = 5;
    private const int MaximumActivityDescriptionLength = 100;
    private const int MinimumUsernameLength = 5;
    private const int MaximumUsernameLength = 20;
    private const int MinimumPasswordLength = 6;
    private const int MaximumPasswordLength = 128;

    public BbqService(IBbqSessionRepository bbqSessionRepository,
        ISessionNotesRepository sessionNotesRepository,
        IMapper mapper, 
        IClaimService claimService, 
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ITemplateService templateService,
        IEmailService emailService)
    {
        _bbqSessionRepository = bbqSessionRepository;
        _sessionNotesRepository = sessionNotesRepository;
        _mapper = mapper;
        _claimService = claimService;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _templateService = templateService;
        _emailService = emailService;
    }

    public async Task<IEnumerable<BbqSessionResponseDto>> GetAllAsync()
    {
        var currentUserId = _claimService.GetUserId();

        var bbqSessions = await _bbqSessionRepository.GetAllAsync(tl => tl.CreatedBy == currentUserId);

        return _mapper.Map<IEnumerable<BbqSessionResponseDto>>(bbqSessions);
    }

    public async Task<CreateBbqSessionResponseDto> CreateAsync(CreateBbqSessionInputDto createBbqSessionInputDto)
    {
        if (createBbqSessionInputDto.Description.Length < MinimumDescriptionLength)
        {
            throw new ValidationException($"BBQ Session description must contain a minimum of {MinimumDescriptionLength} characters");
        }
        
        if (createBbqSessionInputDto.Description.Length > MaximumDescriptionLength)
        {
            throw new ValidationException($"BBq Session description must contain a maximum of {MaximumDescriptionLength} characters");
        }

        var bbqSession = new BbqSession
        {
            Description = createBbqSessionInputDto.Description,
            CreatedBy = _claimService.GetUserId(),
            CreatedOn = DateTime.Now
        };

        var addedBbqSession = await _bbqSessionRepository.AddAsync(bbqSession);

        return new CreateBbqSessionResponseDto
        {
            Id = bbqSession.Id
        };
    }

    public async Task<UpdateBbqSessionResponseDto> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto)
    {
        if (updateBbqSessionInputDto.Description.Length < MinimumDescriptionLength)
        {
            throw new ValidationException($"BBQ Session description must contain a minimum of {MinimumDescriptionLength} characters");
        }

        if (updateBbqSessionInputDto.Description.Length > MaximumDescriptionLength)
        {
            throw new ValidationException($"BBq Session description must contain a maximum of {MaximumDescriptionLength} characters");
        }

        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == id);

        var userId = _claimService.GetUserId();

        if (userId != bbqSession.CreatedBy)
            throw new BadRequestException("The selected list does not belong to you");

        bbqSession.Description = updateBbqSessionInputDto.Description;

        return new UpdateBbqSessionResponseDto
        {
            Id = (await _bbqSessionRepository.UpdateAsync(bbqSession)).Id
        };
    }

    public async Task<BaseResponseDto> DeleteAsync(Guid id)
    {
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == id);

        return new BaseResponseDto
        {
            Id = (await _bbqSessionRepository.DeleteAsync(bbqSession)).Id
        };
    }

    public async Task<IEnumerable<SessionNoteResponseDto>> GetAllByBbqSessionIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {

        var sessionNotes = await _sessionNotesRepository.GetAllAsync(ti => ti.Session.Id == id);

        return _mapper.Map<IEnumerable<SessionNoteResponseDto>>(sessionNotes);
    }

    public async Task<CreateSessionNoteResponseDto> CreateAsync(CreateSessionNoteInputDto createSessionNoteInputDto,
        CancellationToken cancellationToken = default)
    {
        if (createSessionNoteInputDto.Note.Length < MinimumNoteLength)
        {
            throw new ValidationException($"Session Note should have minimum of {MinimumNoteLength} characters");
        }

        if (createSessionNoteInputDto.Note.Length > MaximumNoteLength)
        {
            throw new ValidationException($"\"Session Note should have maximum of {MaximumNoteLength} characters");
        }

        if (createSessionNoteInputDto.ActivityDescription.Length < MinimumActivityDescriptionLength)
        {
            throw new ValidationException($"Session Activity Description should have minimum of {MinimumActivityDescriptionLength} characters");
        }

        if (createSessionNoteInputDto.ActivityDescription.Length > MaximumActivityDescriptionLength)
        {
            throw new ValidationException($"\"Session Activity Description should have maximum of {MaximumActivityDescriptionLength} characters");
        }

        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == createSessionNoteInputDto.BbqSessionId);
        var sessionNote = new SessionNote
        {
            Session = bbqSession,
            ActivityDescription = createSessionNoteInputDto.ActivityDescription,
            Note = createSessionNoteInputDto.Note,
            PitTemperature = createSessionNoteInputDto.PitTemperature,
            MeatTemperature = createSessionNoteInputDto.MeatTemperature
        };

        return new CreateSessionNoteResponseDto
        {
            Id = (await _sessionNotesRepository.AddAsync(sessionNote)).Id
        };
    }

    public async Task<UpdateSessionNoteResponseDto> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto,
        CancellationToken cancellationToken = default)
    {
        if (updateSessionNoteInputDto.Note.Length < MinimumNoteLength)
        {
            throw new ValidationException($"Session Note should have minimum of {MinimumNoteLength} characters");
        }

        if (updateSessionNoteInputDto.Note.Length > MaximumNoteLength)
        {
            throw new ValidationException($"\"Session Note should have maximum of {MaximumNoteLength} characters");
        }

        if (updateSessionNoteInputDto.ActivityDescription.Length < MinimumActivityDescriptionLength)
        {
            throw new ValidationException($"Session Activity Description should have minimum of {MinimumActivityDescriptionLength} characters");
        }

        if (updateSessionNoteInputDto.ActivityDescription.Length > MaximumActivityDescriptionLength)
        {
            throw new ValidationException($"\"Session Activity Description should have maximum of {MaximumActivityDescriptionLength} characters");
        }

        var sessionNote = await _sessionNotesRepository.GetFirstAsync(ti => ti.Id == id);

        sessionNote.ActivityDescription = updateSessionNoteInputDto.ActivityDescription;
        sessionNote.Note = updateSessionNoteInputDto.Note;
        sessionNote.PitTemperature = updateSessionNoteInputDto.PitTemperature;
        sessionNote.MeatTemperature = updateSessionNoteInputDto.MeatTemperature;

        return new UpdateSessionNoteResponseDto
        {
            Id = (await _sessionNotesRepository.UpdateAsync(sessionNote)).Id
        };
    }

    public async Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sessionNote = await _sessionNotesRepository.GetFirstAsync(ti => ti.Id == id);

        return new BaseResponseDto
        {
            Id = (await _sessionNotesRepository.DeleteAsync(sessionNote)).Id
        };
    }

    private async Task<bool> EmailAddressIsUniqueAsync(string email, CancellationToken cancellationToken = new())
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user == null;
    }

    private async Task<bool> UsernameIsUniqueAsync(string username, CancellationToken cancellationToken = new())
    {
        var user = await _userManager.FindByNameAsync(username);

        return user == null;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        if (createUserDto.Username.Length < MinimumUsernameLength)
        {
            throw new ValidationException($"Username should have minimum of {MinimumUsernameLength} characters");
        }

        if (createUserDto.Username.Length > MaximumUsernameLength)
        {
            throw new ValidationException($"\"Username should have maximum of {MaximumUsernameLength} characters");
        }

        if (await UsernameIsUniqueAsync(createUserDto.Username) == false)
        {
            throw new ValidationException("Username is not available");
        }

        if (createUserDto.Password.Length < MinimumPasswordLength)
        {
            throw new ValidationException($"Password should have minimum of {MinimumPasswordLength} characters");
        }

        if (createUserDto.Password.Length > MaximumPasswordLength)
        {
            throw new ValidationException($"\"Password should have maximum of {MaximumPasswordLength} characters");
        }

        if (string.IsNullOrWhiteSpace(createUserDto.Email))
        {
            throw new ValidationException("Email address is not valid");
        }

        if (await EmailAddressIsUniqueAsync(createUserDto.Email) == false)
        {
            throw new ValidationException("Email address is already in use");
        }

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
        if (string.IsNullOrWhiteSpace(confirmEmailInputDto.Token))
        {
            throw new ValidationException("Your verification link is not valid");
        }

        if (string.IsNullOrWhiteSpace(confirmEmailInputDto.UserId))
        {
            throw new ValidationException("Your verification link is not valid");
        }

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
