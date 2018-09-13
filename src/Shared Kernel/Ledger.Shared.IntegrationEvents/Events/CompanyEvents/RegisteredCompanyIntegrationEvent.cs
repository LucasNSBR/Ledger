using System;

namespace Ledger.Shared.IntegrationEvents.Events.CompanyEvents
{
    public class RegisteredCompanyIntegrationEvent : IntegrationEvent, IIntegrationEvent
    {
        Guid CompanyId { get; }
    }
}
