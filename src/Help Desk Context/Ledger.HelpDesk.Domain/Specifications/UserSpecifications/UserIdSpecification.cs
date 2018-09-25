using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.UserSpecifications
{
    public class UserIdSpecification : BaseSpecification<User>
    {
        private readonly Guid _id;

        public UserIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return u => u.Id == _id;
        }
    }
}
