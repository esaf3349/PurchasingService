using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.PipelineBehaviors
{
    internal sealed class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private const int LongDurationThresholdMsecs = 500;

        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > LongDurationThresholdMsecs)
                _logger.LogWarning($"Long running request: {typeof(TRequest).Name} ({_timer.ElapsedMilliseconds} milliseconds) {@request}");

            return response;
        }
    }
}