using System;

namespace Ledger.Shared.IntegrationEvents.Events.CompanyEvents
{
    public class RegisteredCompanyIntegrationEvent : IntegrationEvent
    {
        public Guid CompanyId { get; }
        public string Email { get; }

        public RegisteredCompanyIntegrationEvent(Guid companyId, string email)
        {
            CompanyId = companyId;
            Email = email;
        }
    }
}
