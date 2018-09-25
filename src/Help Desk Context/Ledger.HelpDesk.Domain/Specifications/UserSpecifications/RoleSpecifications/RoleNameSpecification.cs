using System;
using System.Linq.Expressions;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles;
using Ledger.Shared.Specifications;

namespace Ledger.HelpDesk.Domain.Specifications.UserSpecifications.RoleSpecifications
{
    public class RoleNameSpecification : BaseSpecification<UserRole>
    {
        private readonly string _roleName;

        public RoleNameSpecification(string roleName)
        {
            _roleName = roleName;
        }

        public override Expression<Func<UserRole, bool>> ToExpression()
        {
            return r => r.RoleName.ToLower() == _roleName.ToLower();
        }
    }
}
