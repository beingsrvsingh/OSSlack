using Identity.Domain.Events;
using MediatR;
using Shared.Application.Interfaces.Logging;

namespace Identity.Application.Features.User.EventHandlers
{
    public class ForgotPasswordEventHandler : INotificationHandler<CreatedUserEmailEvent>
    {
        private readonly ILoggerService logger;
        public ForgotPasswordEventHandler(ILoggerService logger)
        {
            this.logger = logger;
        }
        public Task Handle(CreatedUserEmailEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInfo($"Sent forgot password email: {notification.Email}");
            return Task.CompletedTask;
        }
    }
}