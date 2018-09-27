using System;

namespace Ledger.Shared.IntegrationEvents.Events.UserEvents
{
    public class UserRemovedFromRoleIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string RoleName { get; }

        public UserRemovedFromRoleIntegrationEvent(Guid userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}
