using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using BBQ.DataAccess.Services;

namespace BBQ.Application.Common.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly IClaimService _claimService;

        public PerformanceBehavior(
            ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
            IClaimService claimService)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _claimService = claimService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 50)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _claimService.GetUserId();

                _logger.LogWarning("N-Teir Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                    requestName, elapsedMilliseconds, userId, request);
            }

            return response;
        }
    }
}
