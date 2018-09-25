using System;

namespace Ledger.Shared.IntegrationEvents.Events.UserEvents
{
    public class UserAddedSupportRoleIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; }
        public string Email { get; }

        public UserAddedSupportRoleIntegrationEvent(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
