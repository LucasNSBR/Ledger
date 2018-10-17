using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.UserServices.IdentityResolver;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeIdentityResolver : IIdentityResolver
    {
        private readonly Guid id = new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3");

        public LedgerIdentityUser GetUser()
        {
            return new LedgerIdentityUser(id);
        }

        public IReadOnlyList<Claim> GetUserClaims()
        {
            return new List<Claim>();
        }

        public Guid GetUserId()
        {
            return id;
        }

        public bool IsAuthenticated()
        {
            return true;
        }
    }
}
