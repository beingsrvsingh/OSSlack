using MediatR;
using Shared.Application.Interfaces.Logging;

namespace Shared.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILoggerService logger;

    public UnhandledExceptionBehaviour(ILoggerService logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogError(ex, "Idenity Application - Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
