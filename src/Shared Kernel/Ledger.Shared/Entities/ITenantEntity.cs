using System;

namespace Ledger.Shared.Entities
{
    public interface ITenantEntity
    {
        Guid TenantId { get; }
    }
}
