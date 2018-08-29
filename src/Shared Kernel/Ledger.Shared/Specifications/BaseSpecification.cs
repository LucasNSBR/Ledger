using System;
using System.Linq.Expressions;
using Ledger.Shared.Entities;

namespace Ledger.Shared.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : Entity<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            return ToExpression()
                .Compile()
                .Invoke(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
