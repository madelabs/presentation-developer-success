using BBQ.Application.Common.Email;
using BBQ.Application.Common.Services;
using MediatR;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.Services;

namespace BBQ.Application.UseCases.BbqSession.CreateBbqSession;
public record CreateBbqSessionCommand(string Description) : IRequest<CreateBbqSessionResponseDto>;
public class CreateBbqSessionCommandHandler : IRequestHandler<CreateBbqSessionCommand, CreateBbqSessionResponseDto>
{
    private readonly IBbqSessionRepository _bbqSessionRepository;
    private readonly IEmailService _emailService;
    private readonly IClaimService _claimService;
    public CreateBbqSessionCommandHandler(IBbqSessionRepository bbqSessionRepository, IEmailService emailService, IClaimService claimService)
    {
        _bbqSessionRepository = bbqSessionRepository;
        _emailService = emailService;
        _claimService = claimService;
    }
    public async Task<CreateBbqSessionResponseDto> Handle(CreateBbqSessionCommand request,
        CancellationToken cancellationToken = default)
    {
        var bbqSession = new DataAccess.Entities.BbqSession()
        {
                Description = request.Description
        };

        var addedBbqSession = await _bbqSessionRepository.AddAsync(bbqSession);
        
        await _emailService.SendEmailAsync(EmailMessage.Create("admin@admin.com",
            $"New BBQ Session Created for this user id: {_claimService.GetUserId()}", "New BBQ Session Created"));

        return new CreateBbqSessionResponseDto
        {
            Id = bbqSession.Id
        };
    }
}
