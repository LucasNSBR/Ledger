using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.RoleSpecifications
{
    public class RoleIdSpecification : BaseSpecification<Role>
    {
        private readonly Guid _id;

        public RoleIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return r => r.Id == _id;
        }
    }
}
