using System;

namespace Ledger.Shared.IntegrationEvents.Events.ActivationEvents
{
    public class RejectedCompanyActivationIntegrationEvent : IntegrationEvent
    {
        public Guid CompanyId { get; }
        public DateTime RejectionDate { get; }

        public RejectedCompanyActivationIntegrationEvent(Guid companyId, DateTime rejectionDate)
        {
            CompanyId = companyId;
            RejectionDate = rejectionDate;
        }
    }
}
