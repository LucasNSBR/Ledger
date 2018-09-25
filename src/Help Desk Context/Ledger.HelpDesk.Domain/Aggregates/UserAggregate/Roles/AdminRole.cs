using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles
{
    public class AdminRole : UserRole
    {
        protected AdminRole()
        {
        }

        public AdminRole(Guid userId) : base(userId, RoleTypes.Admin)
        {
        }
    }
}
