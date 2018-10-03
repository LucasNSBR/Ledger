using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.StateSpecifications
{
    public class StateNameSpecification : BaseSpecification<State>
    {
        private readonly string _name;

        public StateNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<State, bool>> ToExpression()
        {
            return s => s.Name.ToLower().Contains(_name.ToLower());
        }
    }
}
