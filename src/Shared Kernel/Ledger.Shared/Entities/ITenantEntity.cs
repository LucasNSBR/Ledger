using System;

namespace Ledger.Shared.Entities
{
    public interface ITenantEntity
    {
        Guid GetTenantId();
        void SetTenantId(Guid id);
    }
}
