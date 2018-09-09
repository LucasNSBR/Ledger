using Microsoft.AspNetCore.Identity;
using System;

namespace Ledger.Identity.Domain.Models.Aggregates.UserAggregate.User
{
    public class LedgerIdentityUser : IdentityUser<Guid>
    {
        protected LedgerIdentityUser() { }

        public LedgerIdentityUser(string email)
        {
            UserName = email;
            Email = email;
        }
    }
}
