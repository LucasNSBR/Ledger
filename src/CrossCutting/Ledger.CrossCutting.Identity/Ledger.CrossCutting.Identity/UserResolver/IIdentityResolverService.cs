using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver
{
    public interface IIdentityResolverService
    {
        Guid GetUserId();
        bool IsAuthenticated();
        IReadOnlyList<Claim> GetUserClaims();
    }
}
