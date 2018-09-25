using Ledger.Shared.Entities;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.UserSpecifications
{
    public class UserIdSpecification<TUser> : BaseSpecification<TUser> where TUser : Entity<TUser>
    {
        private readonly Guid _id;

        public UserIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<TUser, bool>> ToExpression()
        {
            return u => u.Id == _id;
        }
    }
}
