using BBQ.Application.Common.Email;

namespace BBQ.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
