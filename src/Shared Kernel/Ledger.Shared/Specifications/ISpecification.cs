using Ledger.Shared.Entities;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Specifications
{
    public interface ISpecification<T> where T : Entity<T>
    {
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}
