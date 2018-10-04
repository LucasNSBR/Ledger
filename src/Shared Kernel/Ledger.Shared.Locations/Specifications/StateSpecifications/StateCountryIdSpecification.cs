using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.StateSpecifications
{
    public class StateCountryIdSpecification : BaseSpecification<State>
    {
        private readonly Guid _countryId;

        public StateCountryIdSpecification(Guid countryId)
        {
            _countryId = countryId;
        }

        public override Expression<Func<State, bool>> ToExpression()
        {
            return s => s.CountryId == _countryId;
        }
    }
}
