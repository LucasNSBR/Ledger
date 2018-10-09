using Microsoft.AspNetCore.Http;
using System;

namespace Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver
{
    public class IdentityResolverService : IIdentityResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityResolverService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            if (IsAuthenticated())
            {
                string value = _httpContextAccessor.HttpContext.User.Identity.Name;

                if (Guid.TryParse(value, out Guid id))
                {
                    if (id != Guid.Empty)
                        return id;
                    else
                        throw new InvalidOperationException("Failed to parse user id.");
                }
            }

            throw new InvalidOperationException("User not authenticated.");
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
