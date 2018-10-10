using System;

namespace Ledger.Shared.IntegrationEvents.Events.CompanyEvents
{
    public class RegisteredCompanyIntegrationEvent : IntegrationEvent
    {
        public Guid CompanyId { get; }
        public string Email { get; }
        public Guid TenantId { get; }

        public RegisteredCompanyIntegrationEvent(Guid companyId, string email, Guid tenantId)
        {
            CompanyId = companyId;
            Email = email;
            TenantId = tenantId;
        }
    }
}
