using Ledger.Identity.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.Identity.UserServices.IdentityResolver
{
    public interface IIdentityResolver
    {
        Guid GetUserId();
        LedgerIdentityUser GetUser();
        bool IsAuthenticated();
        IReadOnlyList<Claim> GetUserClaims();
    }
}
