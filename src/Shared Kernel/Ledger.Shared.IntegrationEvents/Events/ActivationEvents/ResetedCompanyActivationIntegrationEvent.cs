using System;

namespace Ledger.Shared.IntegrationEvents.Events.ActivationEvents
{
    public class ResetedCompanyActivationIntegrationEvent : IntegrationEvent
    {
        public Guid CompanyId { get; }
        
        public ResetedCompanyActivationIntegrationEvent(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
