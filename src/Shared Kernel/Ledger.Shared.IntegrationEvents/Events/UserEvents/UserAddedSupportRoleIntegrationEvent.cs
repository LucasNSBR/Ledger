using System;

namespace Ledger.Shared.IntegrationEvents.Events.UserEvents
{
    public class UserAddedSupportRoleIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        public UserAddedSupportRoleIntegrationEvent(Guid id, string email)
        {
            UserId = id;
            Email = email;
        }
    }
}
