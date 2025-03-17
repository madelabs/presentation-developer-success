using BBQ.Application.Common.Email;

namespace BBQ.Application.Common.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
