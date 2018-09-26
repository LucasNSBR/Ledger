using System;
using System.Linq.Expressions;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.Shared.Specifications;

namespace Ledger.HelpDesk.Domain.Specifications.RoleSpecifications
{
    public class RoleNameSpecification : BaseSpecification<Role>
    {
        private readonly string _roleName;

        public RoleNameSpecification(string roleName)
        {
            _roleName = roleName;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return r => r.Name.ToLower() == _roleName.ToLower();
        }
    }
}
