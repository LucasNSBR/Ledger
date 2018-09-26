using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.UserSpecifications
{
    public class UserEmailSpecification : BaseSpecification<User>
    {
        private readonly string _email;

        public UserEmailSpecification(string email)
        {
            _email = email;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return u => u.Email.ToLower() == _email.ToLower();
        }
    }
}
