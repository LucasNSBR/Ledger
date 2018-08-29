using System;
using System.Linq.Expressions;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Shared.Specifications;

namespace Ledger.Activations.Domain.Specifications.ActivationSpecifications
{
    public class ActivationIdSpecification : BaseSpecification<Activation>
    {
        private readonly Guid _id;

        public ActivationIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Activation, bool>> ToExpression()
        {
            return a => a.Id == _id;
        }
    }
}
