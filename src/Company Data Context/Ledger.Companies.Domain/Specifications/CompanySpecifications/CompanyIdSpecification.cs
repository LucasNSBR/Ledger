using System;
using System.Linq.Expressions;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Shared.Specifications;

namespace Ledger.Companies.Domain.Specifications.CompanySpecifications
{
    public class CompanyIdSpecification : BaseSpecification<Company>
    {
        private readonly Guid _id;

        public CompanyIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Company, bool>> ToExpression()
        {
            return c => c.Id == _id;
        }
    }
}
