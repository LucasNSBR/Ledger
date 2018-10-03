using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.CitySpecifications
{
    public class CityIdSpecification : BaseSpecification<City>
    {
        private readonly Guid _id;

        public CityIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<City, bool>> ToExpression()
        {
            return c => c.Id == _id;
        }
    }
}
