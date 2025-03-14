using Shared.Domain.Entities;

namespace Identity.Domain.Events
{
    public class ForgotPasswordEvent : BaseEvent
    {
        public required string Email { get; init; }
    }
}
