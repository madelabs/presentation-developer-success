﻿using Microsoft.Extensions.Logging;
using BBQ.Application.Common.Email;

namespace BBQ.Application.Common.Services;

public class DevEmailService : IEmailService
{
    private readonly ILogger<DevEmailService> _logger;

    public DevEmailService(ILogger<DevEmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        await Task.Delay(100);

        _logger.LogInformation($"Email was sent to: [{emailMessage.ToAddress}]. Body: {emailMessage.Body}");
    }
}
