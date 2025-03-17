using MediatR;
using BBQ.Application.Common.Email;
using BBQ.Application.Common.Services;

namespace BBQ.Application.UseCases.BbqSession.CreateBbqSession.Events;

public class BbqSessionCreatedEmailHandler : INotificationHandler<BbqSessionCreated>
{
    private readonly IEmailService _emailService;

    public BbqSessionCreatedEmailHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task Handle(BbqSessionCreated notification, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(EmailMessage.Create("admin@admin.com",
            $"New BBQ Session Created for this user id: {notification.Payload.UserId}", "New BBQ Session Created"));
        
        return;
    }
}


