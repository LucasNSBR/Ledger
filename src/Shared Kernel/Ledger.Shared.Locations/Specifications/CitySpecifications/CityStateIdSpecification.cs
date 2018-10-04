using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.CitySpecifications
{
    public class CityStateIdSpecification : BaseSpecification<City>
    {
        private readonly Guid _stateId;

        public CityStateIdSpecification(Guid stateId)
        {
            _stateId = stateId;
        }

        public override Expression<Func<City, bool>> ToExpression()
        {
            return c => c.StateId == _stateId;
        }
    }
}
