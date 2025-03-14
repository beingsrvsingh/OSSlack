using Shared.Application.Common.Services.Interfaces;
using Identity.Domain.Events;
using MediatR;

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