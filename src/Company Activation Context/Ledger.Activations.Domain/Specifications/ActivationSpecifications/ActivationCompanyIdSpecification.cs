using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Activations.Domain.Specifications.ActivationSpecifications
{
    public class ActivationCompanyIdSpecification : BaseSpecification<Activation>
    {
        private readonly Guid _companyId;

        public ActivationCompanyIdSpecification(Guid companyId)
        {
            _companyId = companyId;
        }

        public override Expression<Func<Activation, bool>> ToExpression()
        {
            return a => a.CompanyId == _companyId;
        }
    }
}
