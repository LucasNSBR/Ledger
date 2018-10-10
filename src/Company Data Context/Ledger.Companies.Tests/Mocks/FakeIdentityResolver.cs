using System;
using Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeIdentityResolver : IIdentityResolverService
    {
        public Guid GetUserId()
        {
            return new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3");
        }

        public bool IsAuthenticated()
        {
            return true;
        }
    }
}
