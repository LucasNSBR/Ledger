using System;

namespace Ledger.Shared.IntegrationEvents.Events.RoleEvents
{
    public class RoleRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid RoleId { get; }
        public string Name { get; }

        public RoleRegisteredIntegrationEvent(Guid roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }
    }
}
