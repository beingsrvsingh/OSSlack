using Identity.Domain.Events;
using MediatR;
using Shared.Application.Interfaces.Logging;

namespace Identity.Application.Features.User.EventHandlers
{
    public class CreatedUserEmailEventHandler : INotificationHandler<CreatedUserEmailEvent>
    {
        private readonly ILoggerService logger;
        public CreatedUserEmailEventHandler(ILoggerService logger)
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
