using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Ledger.CrossCutting.Identity.Aggregates.UserAggregate
{
    public class User 
    {
        public Guid Id { get; private set; }

        private List<Claim> _claims;
        public IReadOnlyList<Claim> Claims
        {
            get
            {
                return _claims;
            }
        }

        public User(Guid id, IReadOnlyList<Claim> claims = null)
        {
            Id = id;
            _claims = claims?.ToList() ?? new List<Claim>();
        }

        public bool IsInRole(Role role)
        {
            return _claims.Any(r => r.Type == ClaimTypes.Role && r.Value == role.Name);
        }

        public bool HaveClaim(Claim claim)
        {
            return _claims.Any(c => c.Equals(claim));
        }
    }
}
