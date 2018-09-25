using System;

namespace Ledger.Shared.IntegrationEvents.Events.UserEvents
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        public UserRegisteredIntegrationEvent(Guid id, string email)
        {
            UserId = id;
            Email = email;
        }
    }
}
