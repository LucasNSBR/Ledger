using Microsoft.AspNetCore.Http;
using System;

namespace Ledger.Identity.Domain.Services.UserServices.UserResolver
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
                    return id;
            }

            throw new InvalidOperationException("Can't parse user id.");
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
