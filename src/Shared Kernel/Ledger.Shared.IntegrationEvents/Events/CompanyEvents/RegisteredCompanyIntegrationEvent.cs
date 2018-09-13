using System;

namespace Ledger.Shared.IntegrationEvents.Events.CompanyEvents
{
    public class RegisteredCompanyIntegrationEvent : IntegrationEvent, IIntegrationEvent
    {
        public Guid CompanyId { get; }

        public RegisteredCompanyIntegrationEvent(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
