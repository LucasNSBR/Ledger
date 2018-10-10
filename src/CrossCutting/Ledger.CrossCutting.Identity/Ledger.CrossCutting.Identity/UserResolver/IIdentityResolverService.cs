using Ledger.CrossCutting.Identity.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver
{
    public interface IIdentityResolver
    {
        Guid GetUserId();
        User GetUser();
        bool IsAuthenticated();
        IReadOnlyList<Claim> GetUserClaims();
    }
}
