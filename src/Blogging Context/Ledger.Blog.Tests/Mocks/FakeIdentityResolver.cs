using Ledger.CrossCutting.Identity.Aggregates.UserAggregate;
using Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.Blog.Tests.Mocks
{
    public class FakeIdentityResolver : IIdentityResolver
    {
        private Guid id = new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3");

        public User GetUser()
        {
            return new User(id);
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

        //Special method to refresh user Id and test notifications
        public void RefreshId()
        {
            id = Guid.NewGuid();
        }
    }
}
