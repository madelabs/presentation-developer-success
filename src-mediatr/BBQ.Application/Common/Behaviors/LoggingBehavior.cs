using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using BBQ.DataAccess.Services;

namespace BBQ.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest>> _logger;
        private readonly IClaimService _claimService;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest>> logger, IClaimService claimService)
        {
            _logger = logger;
            _claimService = claimService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _claimService.GetUserId();

            _logger.LogInformation("BBQ V3 Request: {Name} {@UserId} {@Request}",
                requestName, userId, request);

            return Task.CompletedTask;
        }
    }
}
