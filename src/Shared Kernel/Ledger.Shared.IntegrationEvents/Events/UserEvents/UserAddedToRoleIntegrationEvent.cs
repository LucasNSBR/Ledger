using System;

namespace Ledger.Shared.IntegrationEvents.Events.UserEvents
{
    public class UserAddedToRoleIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string RoleName { get; }

        public UserAddedToRoleIntegrationEvent(Guid userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}
