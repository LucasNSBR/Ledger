using System;

namespace Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver
{
    public interface IIdentityResolverService
    {
        Guid GetUserId();
        bool IsAuthenticated();
    }
}
