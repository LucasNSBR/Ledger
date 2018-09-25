using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles
{
    public class SupportRole : UserRole
    {
        protected SupportRole()
        {
        }

        public SupportRole(Guid userId) : base(userId, RoleTypes.Support)
        {
        }
    }
}
