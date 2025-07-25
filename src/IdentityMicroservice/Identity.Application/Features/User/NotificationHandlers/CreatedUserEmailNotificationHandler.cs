using Identity.Domain.Events;
using MediatR;
using Shared.Application.Interfaces.Logging;

namespace Identity.Application.Features.User.EventHandlers
{
    public class CreatedUserEmailNotificationHandler : INotificationHandler<CreatedUserEmailEvent>
    {
        private readonly ILoggerService<CreatedUserEmailNotificationHandler> logger;
        public CreatedUserEmailNotificationHandler(ILoggerService<CreatedUserEmailNotificationHandler> logger)
        {
            this.logger = logger;
        }
        public Task Handle(CreatedUserEmailEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInfo($"Sent user email: {notification.Email}");
            return Task.CompletedTask;
        }
    }
}
