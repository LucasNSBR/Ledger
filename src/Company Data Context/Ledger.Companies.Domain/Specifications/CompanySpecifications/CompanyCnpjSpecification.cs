using System;
using System.Linq.Expressions;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Shared.Specifications;

namespace Ledger.Companies.Domain.Specifications.CompanySpecifications
{
    public class CompanyCnpjSpecification : BaseSpecification<Company>
    {
        private readonly string _cnpj;

        public CompanyCnpjSpecification(string cnpj)
        {
            _cnpj = cnpj;
        }

        public override Expression<Func<Company, bool>> ToExpression()
        {
            return c => c.Cnpj.Number == _cnpj;
        }
    }
}
