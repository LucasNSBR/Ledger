using Microsoft.AspNetCore.Identity;
using System;

namespace Ledger.Identity.Domain.Aggregates.RoleAggregate
{
    public class LedgerIdentityRole : IdentityRole<Guid>
    {
        protected LedgerIdentityRole() { }

        public LedgerIdentityRole(string name)
        {
            Name = name;
        }
    }
}
