namespace Ledger.Shared.IntegrationEvents.Events.RoleEvents
{
    public class RoleRemovedIntegrationEvent : IntegrationEvent
    {
        public string RoleName { get; }

        public RoleRemovedIntegrationEvent(string roleName)
        {
            RoleName = roleName;
        }
    }
}
