using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver
{
    public interface IIdentityResolver
    {
        Guid GetUserId();
        bool IsAuthenticated();
        IReadOnlyList<Claim> GetUserClaims();
    }
}
