using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Ledger.Identity.Domain.Aggregates.UserAggregate
{
    public class LedgerIdentityUser : IdentityUser<Guid>
    {
        private List<Claim> _claims;
        public IReadOnlyList<Claim> Claims
        {
            get
            {
                return _claims;
            }
        }

        protected LedgerIdentityUser()
        {
            _claims = new List<Claim>();
        }

        public LedgerIdentityUser(Guid id)
        {
            Id = id;

            _claims = new List<Claim>();
        }

        public LedgerIdentityUser(string email)
        {
            UserName = email;
            Email = email;

            _claims = new List<Claim>();
        }

        public LedgerIdentityUser(Guid id, IReadOnlyList<Claim> claims)
        {
            Id = id;
            _claims = claims.ToList();
        }

        public bool IsInRole(LedgerIdentityRole role)
        {
            return _claims.Any(r => r.Type == ClaimTypes.Role && r.Value == role.Name);
        }

        public bool HaveClaim(Claim claim)
        {
            return _claims.Any(c => c.Equals(claim));
        }
    }
}
