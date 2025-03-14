using Shared.Domain.Entities;

namespace Identity.Domain.Events
{
    public class CreatedUserEmailEvent : BaseEvent
    {
        public required string UserName { get; set; }

        public required string Email { get; set; }
    }
}
