using System;

namespace Ledger.Shared.IntegrationEvents.Events.UserEvents
{
    public class UserAddedSupportRoleIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public UserAddedSupportRoleIntegrationEvent(Guid id)
        {
            UserId = id;
        }
    }
}
