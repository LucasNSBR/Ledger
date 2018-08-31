using System;

namespace Ledger.Shared.IntegrationEvents.Events.ActivationEvents
{
    public class AcceptedCompanyActivationIntegrationEvent : IntegrationEvent
    {
        public Guid CompanyId { get; }
        public DateTime ActivationDate { get; }

        public AcceptedCompanyActivationIntegrationEvent(Guid companyId, DateTime activationDate)
        {
            CompanyId = companyId;
            ActivationDate = activationDate;
        }
    }
}
