using System.Diagnostics;
using MediatR;
using Shared.Application.Common.Services.Interfaces;

namespace Shared.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILoggerService logger;
    private readonly IUserProvider _user;

    public PerformanceBehaviour(
        ILoggerService logger,
        IUserProvider user)
    {
        _timer = new Stopwatch();

        this.logger = logger;
        _user = user;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.UserId ?? string.Empty;
            var userName = _user.UserName ?? string.Empty;

            logger.LogWarning("OSSlack Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
