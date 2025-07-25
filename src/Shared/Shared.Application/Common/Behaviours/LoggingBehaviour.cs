using MediatR.Pipeline;
using Shared.Application.Interfaces.Logging;
using Shared.Application.Interfaces;

namespace Shared.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILoggerService<LoggingBehaviour<TRequest>> logger, IUserProvider user) : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILoggerService<LoggingBehaviour<TRequest>> _logger = logger;
    private readonly IUserProvider _user = user;

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.UserId ?? string.Empty;
        var userName = _user.UserName ?? string.Empty;

        _logger.LogInfo("Identity.Application Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName!, request);
        await Task.CompletedTask;
    }
}
