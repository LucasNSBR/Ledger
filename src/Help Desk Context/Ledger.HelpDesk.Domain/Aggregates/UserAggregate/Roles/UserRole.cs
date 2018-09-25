using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles
{
    public abstract class UserRole : Entity<UserRole>
    {
        public string RoleName { get; }

        protected UserRole() { }

        public UserRole(Guid userId, string roleName)
        {
            Id = userId;
            RoleName = roleName;
        }
    }
}
