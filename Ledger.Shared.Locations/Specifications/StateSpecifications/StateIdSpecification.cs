using Ledger.Shared.Entities.Locations;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.StateSpecifications
{
    public class StateIdSpecification : BaseSpecification<State>
    {
        private readonly Guid _id;

        public StateIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<State, bool>> ToExpression()
        {
            return s => s.Id == _id;
        }
    }
}
