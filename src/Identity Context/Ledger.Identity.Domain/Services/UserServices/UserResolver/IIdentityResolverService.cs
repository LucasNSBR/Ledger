using System;

namespace Ledger.Identity.Domain.Services.UserServices.UserResolver
{
    public interface IIdentityResolverService
    {
        Guid GetUserId();
        bool IsAuthenticated();
    }
}
